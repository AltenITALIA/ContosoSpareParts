using Plugin.Media.Abstractions;
using SpareParts.ApiModel.Vehicles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpareParts.Mobile.Models
{
    public class HistoryData
    {
        public GetVehicle Vehicle { get; }

        public MediaFile File { get; }

        public HistoryData(GetVehicle vehicle, MediaFile file)
        {
            Vehicle = vehicle;
            File = file;
        }
    }
}
