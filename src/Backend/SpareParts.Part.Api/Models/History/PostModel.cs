using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SpareParts.Part.Api.Models.History
{
    public class PostModel
    {
        [Required]
        [JsonProperty("partCode")]
        public string PartCode { get; set; }

        [Required]
        [JsonProperty("vehicleId")]
        public string VehicleId { get; set; }
        
    }
}
