using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepDevDay.ContosoSpareParts.ApiModel.Vehicles
{
    public partial class AddVehicle
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("customer")]
        public string Customer { get; set; }

        [JsonProperty("plate")]
        public string Plate { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }
    }

    public partial class AddVehicle
    {
        public static AddVehicle FromJson(string json) => JsonConvert.DeserializeObject<AddVehicle>(json, DeepDevDay.ContosoSpareParts.ApiModel.Converter.Settings);
    }

}
