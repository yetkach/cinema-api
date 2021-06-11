using AutoMapper;
using Cinema.Logics.Interfaces;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService movieService;
        private readonly IMapper mapper;

        public MoviesController(IMovieService movieService, IMapper mapper)
        {
            this.movieService = movieService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var moviesDto = await movieService.GetAllAsync();
            var movieModels = mapper.Map<List<MovieModel>>(moviesDto);

            return Ok(movieModels);
        }
    }
}
