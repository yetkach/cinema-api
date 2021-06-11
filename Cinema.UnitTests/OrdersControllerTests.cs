using AutoMapper;
using Cinema.Data.EF;
using Cinema.Data.Entities;
using Cinema.Data.Repositories;
using Cinema.Logics.Services;
using Cinema.Web.Controllers;
using Cinema.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Cinema.UnitTests
{
    public class OrdersControllerTests : IDisposable
    {
        private readonly IMapper mapper;
        private readonly CinemaDbContext context;
        public OrdersControllerTests()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
              .UseInMemoryDatabase("CinemaDb").Options;
            context = new CinemaDbContext(options);

            mapper = new MapperConfiguration(config =>
              config.AddProfile(typeof(TestingMapper))
          ).CreateMapper();
        }

        [Fact]
        public async Task CreateAsync_ReturnsOkObjectResult()
        {
            //Arrange 

            var seats = new List<SeatModel>
            {
                new SeatModel(){MovieId = 1, Number = 2, Row = 2},
                new SeatModel(){MovieId = 1, Number = 3, Row = 3}
            };

            var order = new OrderModel()
            {
                GuidId = "guid",
                UserFirstName = "Bob",
                UserLastName = "Bing",
                UserPhoneNumber = "+3803344555",
                Price = 10,
                Seats = seats
            };

            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var orderService = new OrderService(unitOfWork, mapper);
            var controller = new OrdersController(orderService, mapper);

            //Act
            var result = await controller.CreateAsync(order);

            //Assert
            var okObjectResult = (result as OkResult);
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsInvalidOperationException()
        {
            //Arrange 
            var seats = new List<Seat>
            {
                new Seat() {MovieId = 1, Number = 2, Row = 2},
                new Seat() {MovieId = 1, Number = 3, Row = 3}
            };

            await context.AddRangeAsync(seats);
            await context.SaveChangesAsync();

            var selectedSeats = new List<SeatModel>
            {
                new SeatModel(){MovieId = 1, Number = 2, Row = 2},
                new SeatModel(){MovieId = 1, Number = 5, Row = 3}
            };

            var order = new OrderModel()
            {
                GuidId = "guid",
                UserFirstName = "Bob",
                UserLastName = "Bing",
                UserPhoneNumber = "+3803344555",
                Price = 10,
                Seats = selectedSeats
            };

            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var orderService = new OrderService(unitOfWork, mapper);
            var controller = new OrdersController(orderService, mapper);

            //Act and Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => controller.CreateAsync(order));
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
