using System;
using AutoMapper;
using SpareParts.Part.Api.Models.Part;

namespace SpareParts.Part.Api
{
    public class MapperConfig
    {
        public static IMapper Get(IServiceProvider provider)
        {
            return new MapperConfiguration(e =>
            {
                e.CreateMap<Part.ReadModel.Part, GetModel>();
            }).CreateMapper();
        }
    }
}
