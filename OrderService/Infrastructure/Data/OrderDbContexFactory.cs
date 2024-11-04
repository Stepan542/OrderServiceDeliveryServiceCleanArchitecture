using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ApplicationDbContexFactory : IDesignTimeDbContextFactory<OrderDbConext>
    {
        public OrderDbConext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<OrderDbConext>();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Users\makar\Desktop\OrderDeliveryServicesSolution\OrderService\API\appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("OrderDB"));

            return new OrderDbConext(optionsBuilder.Options);
        }
    }
}