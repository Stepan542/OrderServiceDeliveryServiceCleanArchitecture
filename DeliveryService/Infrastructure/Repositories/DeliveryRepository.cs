using Common.Models;
using Infrastructure.Data;
using Domain.Interfaces;

namespace Infrastructure.Repositories
{
    public class DeliveryRepository : BaseRepository<Order>, IDeliveryRepository
    {
        // нужно ли писать crud для сервисов?
        public DeliveryRepository(DeliveryDbContext contex) : base(contex) {}
    }
}