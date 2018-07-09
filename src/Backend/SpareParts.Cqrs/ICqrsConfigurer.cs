using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Handlers;
using Rebus.Routing;
using Rebus.Routing.TypeBased;
using Rebus.ServiceProvider;
using Rebus.Transport;

namespace SpareParts.Cqrs
{
    public interface ICqrsConfigurer
    {
        ICqrsConfigurer AddCommandsRouteFromAssemblyOfType<T>(string queueName)
            where T : ICommand;

        ICqrsConfigurer AddEventsRouteFromAssemblyOfType<T>(string queueName)
            where T : IEvent;

        ICqrsConfigurer AddQueue(string queueName);

        ICqrsConfigurer AddHandlersFromAssemblyOfType<T>()
            where T : IHandleMessages;

        ICqrsConfigurer Subscribe<T>()
            where T : IEvent;
    }

    internal class CqrsConfigurer : ICqrsConfigurer
    {
        private readonly IServiceCollection _services;
        private readonly List<Action<StandardConfigurer<IRouter>>> _routerActions = new List<Action<StandardConfigurer<IRouter>>>();
        private readonly List<Action<StandardConfigurer<ITransport>>> _transportActions = new List<Action<StandardConfigurer<ITransport>>>();
        private readonly List<Type> _subscriptions = new List<Type>();
        private TypeBasedRouterConfigurationExtensions.TypeBasedRouterConfigurationBuilder typeBasedRouterConfigurationBuilder;

        public CqrsConfigurer(IServiceCollection services)
        {
            _services = services;
        }

        public IServiceProvider Provider { get; set; }

        public ICqrsConfigurer AddCommandsRouteFromAssemblyOfType<T>(string queueName)
            where T :ICommand
        {
            _routerActions.Add(r => MapAssemblyOf<T, ICommand>(r, queueName));            

            return this;
        }

        public ICqrsConfigurer AddEventsRouteFromAssemblyOfType<T>(string queueName)
            where T : IEvent
        {
            _routerActions.Add(r => MapAssemblyOf<T, IEvent>(r, queueName));

            return this;
        }

        //private static InMemNetwork inMemNetwork = new InMemNetwork();

        public ICqrsConfigurer AddQueue(string queueName)
        {
            //_transportActions.Add(t => t.UseInMemoryTransport(inMemNetwork, queueName));
            //_transportActions.Add(t => t.UseRabbitMq("amqp://guest:guest@bus:5672", queueName));
            _transportActions.Add(t => t.UseSqlServer(Provider.GetRequiredService<IConfiguration>().GetConnectionString("Rebus"), "Queue", queueName));

            return this;
        }

        public ICqrsConfigurer AddHandlersFromAssemblyOfType<T>() where T : IHandleMessages
        {
            _services.AutoRegisterHandlersFromAssemblyOf<T>();

            return this;
        }

        public ICqrsConfigurer Subscribe<T>() where T : IEvent
        {
            _subscriptions.Add(typeof(T));

            return this;
        }

        public void ApplyActions(StandardConfigurer<IRouter> routerConfigurer)
        {
            foreach (Action<StandardConfigurer<IRouter>> routerAction in _routerActions)
            {
                routerAction(routerConfigurer);
            }
        }

        public void ApplyActions(StandardConfigurer<ITransport> transportConfigurer)
        {
            foreach (Action<StandardConfigurer<ITransport>> transportAction in _transportActions)
            {
                transportAction(transportConfigurer);
            }
        }

        private void MapAssemblyOf<T, TInterface>(StandardConfigurer<IRouter> configurer, string queueName)
        {
            typeBasedRouterConfigurationBuilder = typeBasedRouterConfigurationBuilder ?? configurer.TypeBased();

            foreach (Type type in typeof(T).Assembly.GetTypes().Where(t => typeof(TInterface).IsAssignableFrom(t)))
            {
                typeBasedRouterConfigurationBuilder.Map(type, queueName);
            }
        }

        public async Task ApplySubscriptionsAsync(IBus bus)
        {
            foreach (Type subscription in _subscriptions)
            {
                await bus.Subscribe(subscription);
            }
        }
    }
}
