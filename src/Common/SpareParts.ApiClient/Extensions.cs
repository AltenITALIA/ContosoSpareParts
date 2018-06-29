using SpareParts.ApiClient;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static void AddApiClient(this IServiceCollection services)
        {
            services.AddHttpClient("history", ConfigureClient).AddTypedClient(c => Refit.RestService.For<IHistoryClient>(c));
            services.AddHttpClient("vehicle", ConfigureClient).AddTypedClient(c => Refit.RestService.For<IVehicleClient>(c));
            services.AddHttpClient("part", ConfigureClient).AddTypedClient(c => Refit.RestService.For<IPartClient>(c));
        }

        private static void ConfigureClient(IServiceProvider provider, System.Net.Http.HttpClient client)
        {
            client.BaseAddress = provider.GetRequiredService<IOptions<ApiOptions>>().Value.BaseUri;

            client.DefaultRequestHeaders.Add("Accept", "application/json");
        }
    }
}
