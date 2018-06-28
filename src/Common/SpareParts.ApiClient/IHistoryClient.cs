using SpareParts.ApiModel.History;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.ApiClient
{
    public interface IHistoryClient
    {
        [Post("/history")]
        Task<string> AddAsync([Body]AddHistory history);

        [Get("/history")]
        Task<GetHistory[]> GetAsync();

        [Get("/history/byVehicle/{vehicleId}")]
        Task<GetHistory[]> GetByVehicleAsync(string vehicleId);

        [Put("/history/photo/{id}")]
        Task UploadPhotoAsync(string id, Stream stream);

    }
}
