using SpareParts.Cqrs;
using SpareParts.Part.Cqrs.Commands;
using SpareParts.Part.Cqrs.Events;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddPartQueue(this ICqrsConfigurer configurer)
        {
            configurer.AddQueue(Queues.Part.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddPartCommandsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddCommandsRouteFromAssemblyOfType<AddPartCommand>(Queues.Part.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddPartEventsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddEventsRouteFromAssemblyOfType<PartAddedEvent>(Queues.Part.EventsQueueName);

            return configurer;
        }
    }
}
