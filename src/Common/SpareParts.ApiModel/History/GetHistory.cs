using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.ApiModel.History
{
    public partial class GetHistory
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("partCode")]
        public string PartCode { get; set; }

        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }
    }

    public partial class GetHistory
    {
        public static GetHistory FromJson(string json) => JsonConvert.DeserializeObject<GetHistory>(json, Converter.Settings);
    }
}
