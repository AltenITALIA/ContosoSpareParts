using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepDevDay.ContosoSpareParts.ApiModel.Parts
{
    public partial class GetPart
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("photoUri")]
        public string PhotoUri { get; set; }
    }

    public partial class GetPart
    {
        public static GetPart FromJson(string json) => JsonConvert.DeserializeObject<GetPart>(json, Converter.Settings);
    }
}
