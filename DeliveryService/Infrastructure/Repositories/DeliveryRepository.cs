using Domain.Entities;
using Common.Infrastructure;
using Infrastructure.Data;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DeliveryRepository : BaseRepository<Order>, IDeliveryRepository
    {
        public DeliveryRepository(BaseDbContext contex) : base(contex) {}
    }
}