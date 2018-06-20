using SpareParts.ApiModel.History;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SpareParts.ApiModel.Vehicles;

namespace SpareParts.ApiClient
{
    public interface IVehicleClient
    {
        [Post("/vehicles")]
        Task<string> AddAsync([Body]AddVehicle vehicle);

        [Get("/vehicles")]
        Task<GetVehicle[]> GetAsync();

        [Get("/vehicles/byPlate/{plate}")]
        Task<GetVehicle> GetByPlateAsync(string plate);

        [Delete("/vehicles/{id}")]
        [Multipart]
        Task DeleteAsync(string id);

    }
}
