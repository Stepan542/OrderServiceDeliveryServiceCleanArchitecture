using Domain.Entities;
using Common.Infrastructure;
using Domain.Interfaces;
//using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BaseDbContext contex) : base(contex) {}
    }
}