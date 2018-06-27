using System;
using AutoMapper;

namespace SpareParts.Vehicle.Api
{
    public class MapperConfig
    {
        public static IMapper Get(IServiceProvider provider)
        {
            return new MapperConfiguration(e =>
            {
                e.CreateMap<ReadModel.Movie, Models.Movie.GetModel>();
            }).CreateMapper();
        }
    }
}
