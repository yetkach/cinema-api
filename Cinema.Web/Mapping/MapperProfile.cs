using AutoMapper;
using Cinema.Logics.ModelsDto;
using Cinema.Web.Models;

namespace Cinema.Web.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<MovieDto, MovieModel>();
            CreateMap<SeatDto, SeatModel>();
            CreateMap<OrderModel, OrderDto>();
            CreateMap<SeatModel, SeatDto>();
        }
    }
}
