﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using SpareParts.DataAccessObject;
using SpareParts.EntityFramework.Repository;
using SpareParts.Repository;
using SpareParts.Vehicle.DomainModel;
using SpareParts.Vehicle.EntityFramework.DataAccessObject;
using SpareParts.Vehicle.EntityFramework.DataModel;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddVehicleEntityFrameworkRepositories(this IServiceCollection services, Action<DbContextOptionsBuilder> options, ServiceLifetime lifetime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<VehicleEntities>(options, lifetime);
            services.AddSingleton<IHostedService, EntitiesHostedServices>();

            services.TryAddTransient<IRepository<Vehicle>, DbRepository<Vehicle, VehicleEntities>>();

            return services;
        }

        public static IServiceCollection AddVehicleEntityFrameworkDataAccessObjects(this IServiceCollection services)
        {
            services.TryAddTransient<IDataAccessObject<SpareParts.Vehicle.ReadModel.Vehicle>, VehicleDataAccessObject>();

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
                    var c = scope.ServiceProvider.GetRequiredService<VehicleEntities>().Database.GetDbConnection();
                    DatabaseFacade database = scope.ServiceProvider.GetRequiredService<VehicleEntities>().Database;
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
