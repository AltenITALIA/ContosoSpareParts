using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DeepDevDay.ContosoSpareParts.ApiClient.Tests
{
    public class HistoryClientTest : TestBase
    {

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddApiClient();
        }

        [Fact]
        public async Task AddHistoryAsync()
        {
            var client = ServiceProvider.GetRequiredService<IHistoryClient>();
            string id = await client.AddHistoryAsync(new ApiModel.History.AddHistory
            {
                PartCode = "123",
                VehicleId = "234"
            });
        }
    }
}
