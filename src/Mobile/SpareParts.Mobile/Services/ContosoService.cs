using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpareParts.ApiClient;
using SpareParts.ApiModel.History;
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
            var vehicles = await vehicleClient.GetByPlateAsync(plate);
            //return vehicles;

            var list = new List<GetVehicle>(vehicles);
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);

            return list;
        }

        public async Task<IEnumerable<GetHistory>> GetHistoryAsync(GetVehicle vehicle)
        {
            var history = await historyClient.GetByVehicleAsync(vehicle.Id);
            //return history.OrderByDescending(h => h.Date);

            var list = new List<GetHistory>(history);
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);
            list.AddRange(list);

            foreach (var item in list)
            {
                item.PhotoUri = "http://images5.fanpop.com/image/photos/25600000/DOG-ssssss-dogs-25606625-1024-768.jpg";                
            }

            return list;
        }

        public async Task AddHistoryAsync(GetVehicle vehicle, string partCode, string filePath)
        {
            // Innanzi tutto, caricamento l'elemento nella history.
            var historyId = await historyClient.AddAsync(new AddHistory
            {
                VehicleId = vehicle.Id,
                PartCode = partCode
            });

            // A questo punto, effettua anche l'upload dell'immagine.
            using (var stream = File.OpenRead(filePath))
            {
                await historyClient.UploadPhotoAsync(historyId, stream);
            }
        }
    }
}
