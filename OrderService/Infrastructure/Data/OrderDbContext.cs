using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Common.Infrastructure;

namespace Infrastructure.Data
{
    public class OrderDbConext : BaseDbContext
    {
        public OrderDbConext(DbContextOptions<OrderDbConext> options) : base(options) {}
        public DbSet<Order> Orders { get; set; }
    }
}