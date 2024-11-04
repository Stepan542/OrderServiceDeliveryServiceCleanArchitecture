using AutoMapper;
using Application.Dtos;
using Domain.Entities;

namespace Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}