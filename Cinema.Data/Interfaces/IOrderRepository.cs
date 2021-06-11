using Cinema.Data.Entities;
using System.Threading.Tasks;

namespace Cinema.Data.Interfaces
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
    }
}
