using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SpareParts.ApiModel
{
    public static class Extensions
    {
        public static string Serialize(this object model)
        {
            return JsonConvert.SerializeObject(model, Converter.Settings);
        }
    }

}
