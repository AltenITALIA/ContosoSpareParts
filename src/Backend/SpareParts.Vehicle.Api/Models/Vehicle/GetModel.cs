using Newtonsoft.Json;

namespace SpareParts.Vehicle.Api.Models.Vehicle
{
    public class GetModel
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
}
