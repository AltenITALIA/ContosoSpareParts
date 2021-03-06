﻿using SpareParts.ApiModel.History;
using Refit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SpareParts.ApiModel.Parts;
using SpareParts.ApiModel.Vehicles;

namespace SpareParts.ApiClient
{
    public interface IPartClient
    {
        [Post("/parts")]
        Task AddAsync([Body]AddPart part);

        [Get("/parts")]
        Task<GetPart[]> GetAsync();

        [Get("/parts/{code}")]
        Task<GetPart> GetAsync(string code);

        [Delete("/parts/{code}")]
        [Multipart]
        Task DeleteAsync(string code);

        [Put("/parts/photo/{code}")]
        [Multipart]
        Task UploadPhotoAsync(string code, Stream stream);
    }
}
