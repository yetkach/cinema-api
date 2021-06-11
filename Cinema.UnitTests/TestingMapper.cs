using AutoMapper;
using Cinema.Data.Entities;
using Cinema.Logics.ModelsDto;
using Cinema.Web.Models;

namespace Cinema.UnitTests
{
    public class TestingMapper : Profile
    {
        public TestingMapper()
        {
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieDto, MovieModel>();
            CreateMap<OrderModel, OrderDto>();
            CreateMap<OrderDto, Order>();
            CreateMap<SeatModel, SeatDto>().ReverseMap();
            CreateMap<SeatDto, Seat>().ReverseMap();
        }
    }
}
