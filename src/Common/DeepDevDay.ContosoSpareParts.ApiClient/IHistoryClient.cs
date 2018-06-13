using DeepDevDay.ContosoSpareParts.ApiModel.History;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DeepDevDay.ContosoSpareParts.ApiClient
{
    public interface IHistoryClient
    {
        [Post("/history")]
        Task<string> AddAsync([Body]AddHistory history);

        [Get("/history")]
        Task<GetHistory[]> GetAsync();

        [Put("/history/photo/{id}")]
        [Multipart]
        Task UploadPhotoAsync(string id, Stream stream);

    }
}
