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
    public class MoviesControllerTests : IDisposable
    {
        private readonly IMapper mapper;
        private readonly CinemaDbContext context;
        public MoviesControllerTests()
        {
            var options = new DbContextOptionsBuilder<CinemaDbContext>()
              .UseInMemoryDatabase("CinemaDb").Options;
            context = new CinemaDbContext(options);

            mapper = new MapperConfiguration(config =>
              config.AddProfile(typeof(TestingMapper))
          ).CreateMapper();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsOkObjectResultWithListOfMovieModels()
        {
            //Arrange 
            var movies = new List<Movie>()
            {
                new Movie {Name = "Movie1"},
                new Movie {Name = "Movie2"}
            };

            await context.AddRangeAsync(movies);
            await context.SaveChangesAsync();

            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var movieService = new MovieService(unitOfWork, mapper);
            var controller = new MoviesController(movieService, mapper);

            //Act
            var result = await controller.GetAllAsync();

            //Assert
            var movieModels = (result as OkObjectResult).Value as List<MovieModel>;
            Assert.NotNull(movieModels);
            Assert.Equal(movies.Count, movieModels.Count);
            Assert.All(movieModels, movieModel => Assert.NotNull(movieModel.Id));
            Assert.All(movieModels, movieModel => Assert.NotNull(movieModel.Name));
        }

        [Fact]
        public async Task GetAllAsync_ReturnsKeyNotFoundException()
        {
            //Arrange
            var movieRepository = new MovieRepository(context);
            var seatRepository = new SeatRepository(context);
            var orderRepository = new OrderRepository(context);
            var unitOfWork = new UnitOfWork(context, movieRepository, orderRepository, seatRepository);
            var movieService = new MovieService(unitOfWork, mapper);
            var controller = new MoviesController(movieService, mapper);

            //Act and Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => controller.GetAllAsync());
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
