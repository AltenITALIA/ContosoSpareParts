using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.ApiModel.History
{
    public partial class AddHistory
    {
        [JsonProperty("partCode")]
        public string PartCode { get; set; }

        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }
    }

    public partial class AddHistory
    {
        public static AddHistory FromJson(string json) => JsonConvert.DeserializeObject<AddHistory>(json, SpareParts.ApiModel.Converter.Settings);
    }
}
