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
    public class SeatsController : ControllerBase
    {
        private readonly ISeatService seatService;
        private readonly IMapper mapper;

        public SeatsController(ISeatService seatService, IMapper mapper)
        {
            this.seatService = seatService;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllAsync(int id)
        {
            var seatsDto = await seatService.GetAllAsync(id);
            var seatModels = mapper.Map<List<SeatModel>>(seatsDto);

            return Ok(seatModels);
        }
    }
}
