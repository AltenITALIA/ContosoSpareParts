using DeepDevDay.ContosoSpareParts.ApiModel.History;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DeepDevDay.ContosoSpareParts.ApiClient
{
    public interface IHistoryClient
    {
        [Post("/history")]
        Task<string> AddHistoryAsync([Body]AddHistory history);
    }
}
