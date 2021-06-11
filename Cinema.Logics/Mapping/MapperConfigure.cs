using AutoMapper;
using Cinema.Data.Entities;
using Cinema.Logics.ModelsDto;

namespace Cinema.Logics.Mapping
{
    public class MapperConfigure : Profile
    {
        public MapperConfigure()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<Seat, SeatDto>().ReverseMap();
            CreateMap<OrderDto, Order>();
        }
    }
}
