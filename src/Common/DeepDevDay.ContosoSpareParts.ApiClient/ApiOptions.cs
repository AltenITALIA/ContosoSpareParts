using System;
using System.Collections.Generic;
using System.Text;

namespace DeepDevDay.ContosoSpareParts.ApiClient
{
    public class ApiOptions
    {
        public Uri BaseUri { get; set; } = new Uri("https://contoso-spare-parts.azure-api.net");
    }
}
