using SpareParts.Cqrs;
using SpareParts.Vehicle.Cqrs.Events;
using SpareParts.Vehicle.Cqrs.Handlers;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddVehicleHandlers(this ICqrsConfigurer configurer)
        {
            configurer.AddHandlersFromAssemblyOfType<VehicleHandlers>();

            return configurer;
        }

        public static void VehicleSubscribe(this ICqrsConfigurer configurer)
        {
            configurer.Subscribe<VehicleAddedEvent>();
        }
    }
}
