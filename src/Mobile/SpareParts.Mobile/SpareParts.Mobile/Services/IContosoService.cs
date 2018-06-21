using SpareParts.ApiModel.Vehicles;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Mobile.Services
{
    public interface IContosoService
    {
        Task<IEnumerable<GetVehicle>> SearchVehiclesAsync(string plate);
    }
}
