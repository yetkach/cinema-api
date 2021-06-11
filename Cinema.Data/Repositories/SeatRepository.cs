using Cinema.Data.EF;
using Cinema.Data.Entities;
using Cinema.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories
{
    public class SeatRepository : ISeatRepository
    {
        private readonly CinemaDbContext db;

        public SeatRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task<List<Seat>> GetAllAsync(int movieId)
        {
            return await db.Seats.Where(m => m.MovieId == movieId).ToListAsync();
        }

        public async Task<Seat> GetAsync(int movieId, int? row, int? number)
        {
            return await db.Seats.FirstOrDefaultAsync(seat => seat.MovieId == movieId && seat.Row == row && seat.Number == number);
        }
    }
}
