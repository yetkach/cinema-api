using AutoMapper;
using Cinema.Data.Entities;
using Cinema.Data.Interfaces;
using Cinema.Logics.Interfaces;
using Cinema.Logics.ModelsDto;
using System;
using System.Threading.Tasks;

namespace Cinema.Logics.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task AddAsync(OrderDto orderDto)
        {
            foreach (var seatDto in orderDto.Seats)
            {
                var seat = await unitOfWork.Seats.GetAsync(seatDto.MovieId, seatDto.Row, seatDto.Number);

                if (seat != null)
                {
                    throw new InvalidOperationException("This seat is already taken.");
                }
            }

            var order = mapper.Map<Order>(orderDto);
            order.OrderTime = DateTime.UtcNow;

            foreach (var seat in order.Seats)
            {
                seat.OrderGuid = order.GuidId;
            }

            await unitOfWork.Orders.AddAsync(order);
            await unitOfWork.Complete();
        }
    }
}
