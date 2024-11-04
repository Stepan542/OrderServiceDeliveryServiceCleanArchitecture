using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure
{
    public abstract class BaseDbContext : DbContext
    {
        protected BaseDbContext(DbContextOptions options) : base(options) {}
    }
}