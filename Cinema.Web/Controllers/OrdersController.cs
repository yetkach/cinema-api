using AutoMapper;
using Cinema.Logics.Interfaces;
using Cinema.Logics.ModelsDto;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cinema.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService orderService;
        private readonly IMapper mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            this.orderService = orderService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] OrderModel orderModel)
        {
            var orderDto = mapper.Map<OrderDto>(orderModel);
            await orderService.AddAsync(orderDto);
            return Ok();
        }
    }
}
