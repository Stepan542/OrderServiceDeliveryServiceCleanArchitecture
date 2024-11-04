using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            CreateMap<OrderDto, Order>();
        }
    }
}