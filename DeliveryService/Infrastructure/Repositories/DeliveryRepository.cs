using Domain.Entities;
using Common.Infrastructure;
using Infrastructure.Data;
using Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Repositories
{
    public class DeliveryRepository : BaseRepository<Order>, IDeliveryRepository
    {
        public DeliveryRepository(BaseDbContext contex, ILogger<DeliveryRepository> logger) : base(contex, logger) {}
    }
}