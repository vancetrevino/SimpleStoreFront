using AutoMapper;
using SimpleStoreFront.Data.Entities;
using SimpleStoreFront.ViewModels;

namespace SimpleStoreFront.Data
{
    public class StoreFrontMappingProfile : Profile
    {
        public StoreFrontMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(o => o.OrderId, ex => ex.MapFrom(o => o.Id))
                .ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>()
                .ReverseMap();
        }
    }
}
