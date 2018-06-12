using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepDevDay.ContosoSpareParts.ApiModel.Parts
{
    public partial class AddPart
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public partial class AddPart
    {
        public static AddPart FromJson(string json) => JsonConvert.DeserializeObject<AddPart>(json, Converter.Settings);
    }
}
