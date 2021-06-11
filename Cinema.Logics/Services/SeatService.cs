using AutoMapper;
using Cinema.Data.Interfaces;
using Cinema.Logics.Interfaces;
using Cinema.Logics.ModelsDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Logics.Services
{
    public class SeatService : ISeatService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SeatService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<SeatDto>> GetAllAsync(int movieId)
        {
            var seats = await unitOfWork.Seats.GetAllAsync(movieId);

            if (!seats.Any())
            {
                throw new KeyNotFoundException("There are no reservations for this movie");
            }

            var seatsDto = mapper.Map<List<SeatDto>>(seats);
            return seatsDto;
        }
    }
}
