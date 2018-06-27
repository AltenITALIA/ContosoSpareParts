using SpareParts.Cqrs;
using SpareParts.Vehicle.Cqrs.Commands;
using SpareParts.Vehicle.Cqrs.Events;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddCatalogQueue(this ICqrsConfigurer configurer)
        {
            configurer.AddQueue(Queues.Catalog.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddCatalogCommandsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddCommandsRouteFromAssemblyOfType<AddMovieCommand>(Queues.Catalog.QueueName);

            return configurer;
        }

        public static ICqrsConfigurer AddCatalogEventsRoute(this ICqrsConfigurer configurer)
        {
            configurer.AddEventsRouteFromAssemblyOfType<MovieAddedEvent>(Queues.Catalog.EventsQueueName);

            return configurer;
        }
    }
}
