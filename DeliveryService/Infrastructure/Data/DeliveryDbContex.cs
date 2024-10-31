using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace Infrastructure.Data
{
    public class DeliveryDbContext : DbContext
    {
        public DeliveryDbContext(DbContextOptions<DeliveryDbContext> options)
            : base(options) {}

        public DbSet<Order> OrderDeliveries { get; set; }
    }
}