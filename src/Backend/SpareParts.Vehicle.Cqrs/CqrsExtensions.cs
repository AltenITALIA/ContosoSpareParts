using SpareParts.Cqrs;
using SpareParts.Vehicle.Cqrs.Commands;
using SpareParts.Vehicle.Cqrs.Events;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddVehicleQueue(this ICqrsConfigurer configurer)
        {
            configurer.AddQueue(Queues.Vehicle.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddVehicleCommandsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddCommandsRouteFromAssemblyOfType<AddVehicleCommand>(Queues.Vehicle.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddVehicleEventsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddEventsRouteFromAssemblyOfType<VehicleAddedEvent>(Queues.Vehicle.EventsQueueName);

            return configurer;
        }
    }
}
