using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Common.Infrastructure;

namespace Infrastructure.Data
{
    public class DeliveryDbContext : BaseDbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options) {}

        public DbSet<Order> OrderDeliveries { get; set; }
    }
}