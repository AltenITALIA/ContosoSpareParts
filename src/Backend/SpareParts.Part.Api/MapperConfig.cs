using System;
using AutoMapper;

namespace SpareParts.Part.Api
{
    public class MapperConfig
    {
        public static IMapper Get(IServiceProvider provider)
        {
            return new MapperConfiguration(e =>
            {
                e.CreateMap<Part.ReadModel.Part, Api.Models.Part.GetModel>();
                e.CreateMap<Part.ReadModel.History, Api.Models.History.GetModel>();
            }).CreateMapper();
        }
    }
}
