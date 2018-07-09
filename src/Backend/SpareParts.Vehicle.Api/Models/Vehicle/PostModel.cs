using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SpareParts.Vehicle.Api.Models.Vehicle
{
    public class PostModel
    {
        [Required]
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [Required]
        [JsonProperty("model")]
        public string Model { get; set; }

        [Required]
        [JsonProperty("customer")]
        public string Customer { get; set; }

        [Required]
        [JsonProperty("plate")]
        public string Plate { get; set; }

        [Required]
        [JsonProperty("color")]
        public string Color { get; set; }

        [Required]
        [JsonProperty("year")]
        public long Year { get; set; }
    }
}
