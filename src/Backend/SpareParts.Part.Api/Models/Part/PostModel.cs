using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace SpareParts.Part.Api.Models.Part
{
    public class PostModel
    {
        [Required]
        [JsonProperty("code")]
        public string Code { get; set; }

        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }
        
    }
}
