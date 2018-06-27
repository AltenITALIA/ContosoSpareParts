using System;
using System.Security.Claims;
using Microsoft.Extensions.DependencyInjection;
using SpareParts.Common;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddUser(this IServiceCollection services, Func<IServiceProvider, ClaimsPrincipal> getUser)
        {
            services.AddScoped<IUserAccessor>(c =>
            {
                var userAccessor = new UserAccessor ();
                if (userAccessor.User == null) userAccessor.User = getUser(c);
                return userAccessor;
            });

            return services;
        }
        
    }
}
