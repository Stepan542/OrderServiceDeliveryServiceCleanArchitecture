using Microsoft.EntityFrameworkCore;
using Common.Models;

namespace Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {}
        public DbSet<Order> Orders { get; set; }
    }
}