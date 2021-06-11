using Cinema.Logics.ModelsDto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cinema.Logics.Interfaces
{
    public interface ISeatService
    {
        Task<List<SeatDto>> GetAllAsync(int movieId);
    }
}
