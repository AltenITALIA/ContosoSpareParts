using System;
using System.IO;
using System.Threading.Tasks;
using DeepDevDay.ContosoSpareParts.ApiModel.Parts;
using DeepDevDay.ContosoSpareParts.ApiModel.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace DeepDevDay.ContosoSpareParts.ApiClient.Tests
{
    public class PartClientTest : TestBase
    {

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddApiClient();
        }

        [Fact]
        public async Task AddAsync()
        {
            var client = ServiceProvider.GetRequiredService<IPartClient>();
            await client.AddAsync(new AddPart
            {
                Code = "specchietto",
                Name = "Specchietto"
            });
        }

        [Fact]
        public async Task GetAsync()
        {
            var client = ServiceProvider.GetRequiredService<IPartClient>();
            var parts = await client.GetAsync();
            Assert.NotNull(parts);
        }

        [Fact]
        public async Task GetAsyncByCode()
        {
            var client = ServiceProvider.GetRequiredService<IPartClient>();
            var part = await client.GetAsync("specchietto");
            Assert.NotNull(part);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var client = ServiceProvider.GetRequiredService<IPartClient>();
            await client.DeleteAsync("specchietto");
        }
    }
}
