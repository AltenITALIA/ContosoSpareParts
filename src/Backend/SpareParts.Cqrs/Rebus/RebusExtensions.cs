using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Activation;
using Rebus.Bus;
using Rebus.Config;
using Rebus.Messages;
using Rebus.Pipeline;
using Rebus.Pipeline.Receive;
using Rebus.Pipeline.Send;
using Rebus.Retry.Simple;
using SpareParts.Cqrs.Rebus;

// ReSharper disable once CheckNamespace
namespace Rebus.Config
{
    public static class Extensions
    {

        public static void IncludePrincipalClaims(this OptionsConfigurer configurer, IServiceProvider provider)
        {
            configurer.Decorate<IPipeline>(c =>
            {
                var pipeline = c.Get<IPipeline>();
                var step = provider.GetRequiredService<IncludePrincipalClaimsStep>();

                return new PipelineStepInjector(pipeline)
                    .OnSend(step, PipelineRelativePosition.Before, typeof(AutoHeadersOutgoingStep));
            });
            configurer.Decorate<IPipeline>(c =>
            {
                var pipeline = c.Get<IPipeline>();
                var step = provider.GetRequiredService<IncludePrincipalClaimsStep>();

                return new PipelineStepInjector(pipeline)
                    .OnReceive(step, PipelineRelativePosition.Before, typeof(DispatchIncomingMessageStep));
            });
        }

    }
}

// ReSharper disable once CheckNamespace
namespace Rebus.Bus
{
    public static class Extensions
    {
        private static readonly TimeSpan[] RetryDelays = {
            TimeSpan.FromSeconds(5),
            TimeSpan.FromMinutes(5),
            TimeSpan.FromHours(1),
        };

        public static async Task ExponentialRetry<T>(this IBus bus, IFailed<T> failedMessage, int maxRetries)
        {
            if (failedMessage.Headers.TryGetValue(Headers.CorrelationSequence, out string seq) && Int32.TryParse(seq, out int retries) && maxRetries + 2 >= retries)
            {
                retries -= 2;
                TimeSpan delay = RetryDelays.Length >= retries ? RetryDelays[retries] : RetryDelays.Last();

                await bus.Defer(delay, failedMessage.Message);
            }
        }
    }
}