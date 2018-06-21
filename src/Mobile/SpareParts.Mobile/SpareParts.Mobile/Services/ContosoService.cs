using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpareParts.ApiClient;
using SpareParts.ApiModel.Vehicles;

namespace SpareParts.Mobile.Services
{
    public class ContosoService : IContosoService
    {
        private readonly IVehicleClient vehicleClient;
        private readonly IHistoryClient historyClient;

        public ContosoService(IVehicleClient vehicleClient, IHistoryClient historyClient)
        {
            this.vehicleClient = vehicleClient;
            this.historyClient = historyClient;
        }

        public async Task<IEnumerable<GetVehicle>> SearchVehiclesAsync(string plate)
        {
            var vehicle = await vehicleClient.GetByPlateAsync(plate);
            return new List<GetVehicle>() { vehicle };
        }
    }
}
