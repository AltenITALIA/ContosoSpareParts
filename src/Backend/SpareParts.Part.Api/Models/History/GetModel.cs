using System;
using Newtonsoft.Json;

namespace SpareParts.Part.Api.Models.History
{
    public class GetModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public DateTime Date { get; set; }

        [JsonProperty("partCode")]
        public string PartCode { get; set; }

        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }

        [JsonProperty("photoUri")]
        public string PhotoUri { get; set; }
    }
}
