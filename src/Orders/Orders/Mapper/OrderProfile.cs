using AutoMapper;
using Orders.Domain;
using Orders.Dtos;

namespace Orders.Mapper
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, CreateOrderDto>().ReverseMap();
        }
    }
}
