using Cinema.Data.EF;
using Cinema.Data.Entities;
using Cinema.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories
{
    public class MovieRepository : IMovieRepository
    {
        private readonly CinemaDbContext db;

        public MovieRepository(CinemaDbContext db)
        {
            this.db = db;
        }
        public async Task<List<Movie>> GetAllAsync()
        {
            return await db.Movies.ToListAsync();
        }
    }
}
