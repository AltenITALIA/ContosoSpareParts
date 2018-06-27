using SpareParts.Cqrs;
using SpareParts.Part.Cqrs.Events;
using SpareParts.Part.Cqrs.Handlers;


// ReSharper disable once CheckNamespace
namespace SpareParts.Cqrs
{
    public static class CqrsExtensions
    {
        public static ICqrsConfigurer AddPartHandlers(this ICqrsConfigurer configurer)
        {
            configurer.AddHandlersFromAssemblyOfType<PartHandlers>();

            return configurer;
        }

        public static void PartSubscribe(this ICqrsConfigurer configurer)
        {
           
        }
    }
}
