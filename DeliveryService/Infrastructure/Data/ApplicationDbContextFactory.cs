using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class ApplicationDbContexFactory : IDesignTimeDbContextFactory<DeliveryDbContext>
    {
        public DeliveryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DeliveryDbContext>();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(@"C:\Users\makar\Desktop\OrderDeliveryServicesSolution\DeliveryService\API\appsettings.json")
                .Build();

            optionsBuilder.UseNpgsql(config.GetConnectionString("DeliveryDb"));

            return new DeliveryDbContext(optionsBuilder.Options);
        }
    }
}