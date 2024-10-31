using AutoMapper;
using Common.Interfaces;
using Common.Models;

namespace Application.Mappers
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            // Dto был бы лучше
            CreateMap<IOrderForDelivery, Order>();
        }
    }
}