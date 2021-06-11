using Cinema.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Data.Interfaces
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetAllAsync(int movieId);
        Task<Seat> GetAsync(int movieId, int? row, int? number);
    }
}
