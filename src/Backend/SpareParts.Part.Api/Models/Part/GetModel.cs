using Newtonsoft.Json;

namespace SpareParts.Part.Api.Models.Part
{
    public class GetModel
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUri")]
        public string PhotoUri { get; set; }
    }
}
