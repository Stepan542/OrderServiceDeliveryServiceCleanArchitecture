using Infrastructure.Data;
using Common.Models;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ApplicationDbContext dbContext) 
            : base(dbContext) {}
    }
}