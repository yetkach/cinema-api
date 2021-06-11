using Cinema.Logics.ModelsDto;
using System.Threading.Tasks;

namespace Cinema.Logics.Interfaces
{
    public interface IOrderService
    {
        Task AddAsync(OrderDto orderDto);
    }
}
