using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SpareParts.ApiClient.Tests
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
            string id = await client.AddAsync(new ApiModel.History.AddHistory
            {
                PartCode = "123",
                VehicleId = "234"
            });
            Assert.NotNull(id);
        }

        [Fact]
        public async Task GetHistoryAsync()
        {
            var client = ServiceProvider.GetRequiredService<IHistoryClient>();
            var histories = await client.GetAsync();
            Assert.NotNull(histories);
        }

        [Fact]
        public async Task GetHistoryByVehicleAsync()
        {
            var client = ServiceProvider.GetRequiredService<IHistoryClient>();
            var histories = await client.GetByVehicleAsync("11");
            Assert.NotNull(histories);
        }

        [Fact]
        public async Task UploadHistoryPhotoAsync()
        {
            var client = ServiceProvider.GetRequiredService<IHistoryClient>();
            await client.UploadPhotoAsync("1", new MemoryStream(new byte[1]));
        }
    }
}
