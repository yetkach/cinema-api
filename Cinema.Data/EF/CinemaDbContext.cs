using Cinema.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Cinema.Data.EF
{
    public class CinemaDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Movie> Movies { get; set; }

        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var movies = new List<Movie>()
            {
                new Movie {Id = 1, Name = "Cruella" },
                new Movie {Id = 2, Name = "Quiet Place"},
            };

            modelBuilder.Entity<Movie>().HasData(movies);
        }
    }
}
