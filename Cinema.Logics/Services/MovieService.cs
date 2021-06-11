using AutoMapper;
using Cinema.Data.Interfaces;
using Cinema.Logics.Interfaces;
using Cinema.Logics.ModelsDto;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Logics.Services
{
    public class MovieService : IMovieService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public MovieService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<List<MovieDto>> GetAllAsync()
        {
            var movies = await unitOfWork.Movies.GetAllAsync();

            if (!movies.Any())
            {
                throw new KeyNotFoundException("There are currently no movies to watch");
            }

            var moviesDto = mapper.Map<List<MovieDto>>(movies);
            return moviesDto;
        }
    }
}
