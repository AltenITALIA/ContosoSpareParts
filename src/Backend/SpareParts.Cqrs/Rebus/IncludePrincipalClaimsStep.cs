using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Messages;
using Rebus.Pipeline;
using SpareParts.Common;

namespace SpareParts.Cqrs.Rebus
{
    internal class IncludePrincipalClaimsStep : IOutgoingStep, IIncomingStep
    {
        private readonly IServiceProvider _serviceProvider;
        private const string Prefix = "x-c-";
        private const string AuthenticationTypeKey = "x-at";

        public IncludePrincipalClaimsStep(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Process(IncomingStepContext context, Func<Task> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userAccessor = scope.ServiceProvider.GetRequiredService<IUserAccessor>();
                var message = context.Load<Message>();

                if (message.Headers.ContainsKey(AuthenticationTypeKey))
                {
                    var claims = message.Headers
                        .Where(p => p.Key.StartsWith(Prefix))
                        .Select(p => new Claim(p.Key.Substring(Prefix.Length), p.Value))
                        .ToArray();

                    userAccessor.User = new ClaimsPrincipal(new ClaimsIdentity(claims, message.Headers[AuthenticationTypeKey]));
                }
            }

            await next();
        }

        public async Task Process(OutgoingStepContext context, Func<Task> next)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var userAccessor = scope.ServiceProvider.GetRequiredService<IUserAccessor>();
                if (userAccessor.User != null)
                {
                    var message = context.Load<Message>();

                    var claims = userAccessor.User.Claims;
                    foreach (var c in claims)
                    {
                        message.Headers[Prefix + c.Type] = c.Value;
                    }

                    message.Headers[AuthenticationTypeKey] = userAccessor.User.Identity.AuthenticationType;
                }
            }

            await next();
        }
    }
}
