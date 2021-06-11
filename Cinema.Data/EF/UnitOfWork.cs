using Cinema.Data.Interfaces;
using System.Threading.Tasks;

namespace Cinema.Data.EF
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CinemaDbContext db;
        public IMovieRepository Movies { get; }
        public ISeatRepository Seats { get; }
        public IOrderRepository Orders { get; }

        public UnitOfWork(CinemaDbContext db, IMovieRepository movieRepository,
            IOrderRepository orderRepository,
            ISeatRepository seatRepository)
        {
            this.db = db;
            Movies = movieRepository;
            Seats = seatRepository;
            Orders = orderRepository;
        }

        public async Task Complete()
        {
            await db .SaveChangesAsync();
        }
    }
}
