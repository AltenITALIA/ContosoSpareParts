using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepDevDay.ContosoSpareParts.ApiModel
{
    public static class Extensions
    {
        public static string Serialize(this object model)
        {
            return JsonConvert.SerializeObject(model, Converter.Settings);
        }
    }

}
