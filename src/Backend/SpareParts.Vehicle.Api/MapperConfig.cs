using System;
using AutoMapper;
using SpareParts.Vehicle.Api.Models.Vehicle;

namespace SpareParts.Vehicle.Api
{
    public class MapperConfig
    {
        public static IMapper Get(IServiceProvider provider)
        {
            return new MapperConfiguration(e =>
            {
                e.CreateMap<ReadModel.Vehicle, GetModel>();
            }).CreateMapper();
        }
    }
}
