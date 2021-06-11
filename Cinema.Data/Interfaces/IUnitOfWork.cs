using System.Threading.Tasks;

namespace Cinema.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IMovieRepository Movies { get; }
        ISeatRepository Seats { get; }
        IOrderRepository Orders { get; }
        Task Complete();
    }
}
