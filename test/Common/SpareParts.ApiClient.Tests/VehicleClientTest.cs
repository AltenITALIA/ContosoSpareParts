using System;
using System.IO;
using System.Threading.Tasks;
using SpareParts.ApiModel.Vehicles;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace SpareParts.ApiClient.Tests
{
    public class VehicleClientTest : TestBase
    {

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            services.AddApiClient();
        }

        [Fact]
        public async Task AddAsync()
        {
            var client = ServiceProvider.GetRequiredService<IVehicleClient>();
            string id = await client.AddAsync(new AddVehicle
            {
                Brand = "BMW",
                Color = "Black",
                Customer = "Marco Leoncini",
                Model = "X5",
                Plate = "123456",
                Year = 2017
            });
            Assert.NotNull(id);
        }

        [Fact]
        public async Task GetAsync()
        {
            var client = ServiceProvider.GetRequiredService<IVehicleClient>();
            var vehicles = await client.GetAsync();
            Assert.NotNull(vehicles);
        }

        [Fact]
        public async Task GetByPlateAsync()
        {
            var client = ServiceProvider.GetRequiredService<IVehicleClient>();
            var vehicle = await client.GetByPlateAsync("123456");
            Assert.NotNull(vehicle);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var client = ServiceProvider.GetRequiredService<IVehicleClient>();
            await client.DeleteAsync("123456");
        }
    }
}
