using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Retry.Simple;
using Rebus.Serialization.Json;
using Rebus.ServiceProvider;
using SpareParts.Cqrs;
using SpareParts.Cqrs.Rebus;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServiceBus(this IServiceCollection services, Action<ICqrsConfigurer> configurerAction)
        {
            // Numero di richieste contemporaneamente gestite
            int w = Environment.ProcessorCount * 10;

            var configurer = new CqrsConfigurer(services);
            configurerAction(configurer);

            services.AddTransient<IncludePrincipalClaimsStep>();

            services.AddRebus((configure, provider) =>
            {
                configurer.Provider = provider;
                string connectionString = provider.GetRequiredService<IConfiguration>().GetConnectionString("Rebus");
                //EnsureDatabaseCreated(connectionString);

                return configure
                    .Serialization(s => s.UseNewtonsoftJson())
                    .Options(o =>
                    {
                        o.EnableSynchronousRequestReply(replyMaxAgeSeconds: 30);
                        o.SetNumberOfWorkers(w);
                        o.SetMaxParallelism(w);
                        o.IncludePrincipalClaims(provider);
                        o.SetDueTimeoutsPollInteval(TimeSpan.FromSeconds(1));
                        o.SimpleRetryStrategy(maxDeliveryAttempts: 1, secondLevelRetriesEnabled: true);
                    })
                    //.Sagas(s => s.StoreInSqlServer(connectionString, "Sagas", "SagasIndex"))
                    //.Timeouts(t => t.StoreInSqlServer(connectionString, "MessageTimeouts"))
                    .Subscriptions(t => t.StoreInSqlServer(connectionString, "Subscriptions"))
                    .Logging(l => l.Serilog())
                    .Transport(configurer.ApplyActions)
                    .Routing(configurer.ApplyActions);
            });

            services.AddSingleton<IHostedService, RebusHostedService>(r => new RebusHostedService(r, configurer));

            return services;
        }

        //private static void EnsureDatabaseCreated(string connectionString)
        //{
        //    var builder = new SqlConnectionStringBuilder(connectionString);
        //    string databaseName = builder.InitialCatalog;
        //    builder.InitialCatalog = "master";
        //    using (var connection = new SqlConnection(builder.ConnectionString))
        //    {
        //        connection.Open();
        //        using (SqlCommand command = connection.CreateCommand())
        //        {
        //            command.CommandText = $@"IF (NOT EXISTS (SELECT name 
        //            FROM master.dbo.sysdatabases
        //            WHERE(name = @n))) BEGIN CREATE DATABASE [{databaseName}]; END";

        //            SqlParameter sqlParameter = command.CreateParameter();
        //            sqlParameter.ParameterName = "@n";
        //            sqlParameter.Value = databaseName;

        //            command.Parameters.Add(sqlParameter);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        private class RebusHostedService : IHostedService
        {
            private readonly IServiceProvider _serviceProvider;
            private readonly CqrsConfigurer _cqrsConfigurer;

            public RebusHostedService(IServiceProvider serviceProvider, CqrsConfigurer cqrsConfigurer)
            {
                _serviceProvider = serviceProvider;
                _cqrsConfigurer = cqrsConfigurer;
            }

            public async Task StartAsync(CancellationToken cancellationToken)
            {
                // Attiva il bus
                var bus = _serviceProvider.GetRequiredService<IBus>();

                await _cqrsConfigurer.ApplySubscriptionsAsync(bus);
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
