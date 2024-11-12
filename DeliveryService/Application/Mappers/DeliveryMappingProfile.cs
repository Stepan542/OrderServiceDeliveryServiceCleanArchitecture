using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappers
{
    public class DeliveryMappingProfile : Profile
    {
        public DeliveryMappingProfile()
        {
            // сейчас он бесполезен, так как происхоодит явное преобразование в сервисах
            CreateMap<OrderDto, Order>();
        }
    }
}