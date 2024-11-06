using Domain.Entities;
using Common.Infrastructure;
using Domain.Interfaces;
//using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BaseDbContext contex, ILogger<OrderRepository> logger)
            : base(contex, logger) {}
    }
}