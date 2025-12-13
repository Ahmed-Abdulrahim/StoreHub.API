using AutoMapper;
using StoreHub.Application.Dtos.OrderDto;
using StoreHub.Core.Models.Identity;
using StoreHub.Core.Models.Orders;


namespace StoreHub.Application.MappingProfile
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.ProductId))
                .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.ProductName))
                .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.Product.PictureUrl)).ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(d => d.PaymentStatus, o => o.MapFrom(s => s.PaymentStatus.ToString()))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.Total, o => o.MapFrom(s => s.SubTotal + s.DeliveryMethod.Cost)).ReverseMap();

            CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

            CreateMap<ShippingAddress, ShippingAddressDto>().ReverseMap();
            CreateMap<ShippingAddressDto, Address>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.AppUserId, opt => opt.Ignore()).ReverseMap();
        }
    }
}
