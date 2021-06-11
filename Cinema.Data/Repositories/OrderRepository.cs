using Cinema.Data.EF;
using Cinema.Data.Entities;
using Cinema.Data.Interfaces;
using System.Threading.Tasks;

namespace Cinema.Data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CinemaDbContext db;

        public OrderRepository(CinemaDbContext db)
        {
            this.db = db;
        }

        public async Task AddAsync(Order order)
        {
            await db.Orders.AddAsync(order);
        }
    }
}
