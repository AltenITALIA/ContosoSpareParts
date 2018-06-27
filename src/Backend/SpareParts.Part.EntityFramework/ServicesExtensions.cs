using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SpareParts.DataAccessObject;
using SpareParts.EntityFramework.Repository;
using SpareParts.Part.DomainModel;
using SpareParts.Part.EntityFramework.DataAccessObject;
using SpareParts.Part.EntityFramework.DataModel;
using SpareParts.Repository;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddPartEntityFrameworkRepositories(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<PartEntities>(options, lifetime);
            services.AddSingleton<IHostedService, EntitiesHostedServices>();

            services.TryAddTransient<IRepository<Part>, DbRepository<Part, PartEntities>>();

            return services;
        }

        public static IServiceCollection AddPartEntityFrameworkDataAccessObjects(this IServiceCollection services)
        {
            services.TryAddTransient<IDataAccessObject<SpareParts.Part.ReadModel.Part>, PartDataAccessObject>();

            return services;
        }

        private class EntitiesHostedServices : IHostedService
        {
            private readonly IServiceProvider _serviceProvider;

            public EntitiesHostedServices(IServiceProvider serviceProvider)
            {
                _serviceProvider = serviceProvider;
            }

            public async Task StartAsync(CancellationToken cancellationToken)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var c = scope.ServiceProvider.GetRequiredService<PartEntities>().Database.GetDbConnection();
                    DatabaseFacade database = scope.ServiceProvider.GetRequiredService<PartEntities>().Database;
                    //await database.EnsureDeletedAsync(cancellationToken);
                    await database.EnsureCreatedAsync(cancellationToken);
                }
            }

            public Task StopAsync(CancellationToken cancellationToken)
            {
                return Task.CompletedTask;
            }
        }
    }
}
