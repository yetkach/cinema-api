using Cinema.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Data.Interfaces
{
    public interface IMovieRepository
    {
        Task<List<Movie>> GetAllAsync();
    }
}
