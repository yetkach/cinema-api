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
    public class SeatsControllerTests : IDisposable
    {
        private readonly IMapper mapper;
        private readonly CinemaDbContext context;
        public SeatsControllerTests()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
              .UseInMemoryDatabase("CinemaDb").Options;
            context = new CinemaDbContext(options);

            mapper = new MapperConfiguration(config =>
              config.AddProfile(typeof(TestingMapper))
          ).CreateMapper();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsKeyNotFoundException()
        {
            //Arrange 
            var seats = new List<Seat>
            {
                new Seat() {MovieId = 1, Number = 2, Row = 2},
                new Seat() {MovieId = 1, Number = 3, Row = 3}
            };

            await context.AddRangeAsync(seats);
            await context.SaveChangesAsync();

            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var seatService = new SeatService(unitOfWork, mapper);
            var controller = new SeatsController(seatService, mapper);
            int id = 15;

            //Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.GetAllAsync(id));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResultWithListOfSeatModels()
        {
            //Arrange 
            var seats = new List<Seat>
            {
                new Seat() {MovieId = 1, Number = 2, Row = 2},
                new Seat() {MovieId = 1, Number = 3, Row = 3}
            };

            await context.AddRangeAsync(seats);
            await context.SaveChangesAsync();

            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var seatService = new SeatService(unitOfWork, mapper);
            var controller = new SeatsController(seatService, mapper);
            int id = 1;

            //Act
            var result = await controller.GetAllAsync(id);

            //Assert
            var seatModels = (result as OkObjectResult).Value as List<SeatModel>;
            Assert.NotNull(seatModels);
            Assert.Equal(seats.Count, seatModels.Count);
            Assert.All(seatModels, seatModel => Assert.NotNull(seatModel.Number));
            Assert.All(seatModels, seatModel => Assert.NotNull(seatModel.Row));
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
