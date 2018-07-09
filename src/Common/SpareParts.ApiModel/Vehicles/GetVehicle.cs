using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.ApiModel.Vehicles
{
    public partial class GetVehicle
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }
    }

    public partial class GetVehicle
    {
        public static GetVehicle FromJson(string json) => JsonConvert.DeserializeObject<GetVehicle>(json, Converter.Settings);
    }

}
