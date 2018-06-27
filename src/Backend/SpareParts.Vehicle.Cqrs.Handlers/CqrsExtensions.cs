using SpareParts.Cqrs;
using SpareParts.Vehicle.Cqrs.Events;
using SpareParts.Vehicle.Cqrs.Handlers;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddCatalogHandlers(this ICqrsConfigurer configurer)
        {
            configurer.AddHandlersFromAssemblyOfType<MovieHandlers>();

            return configurer;
        }

        public static void CatalogSubscribe(this ICqrsConfigurer configurer)
        {
            configurer.Subscribe<MovieAddedEvent>();
        }
    }
}
