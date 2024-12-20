using AutoMapper;
using backend.DTOs;
using backend.DTOs.CartItem;
using backend.DTOs.Image;
using backend.DTOs.Order;
using backend.DTOs.Product;
using backend.DTOs.UserAdress;
using backend.DTOs.UserPayment;
using backend.Models;

namespace backend;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<PostProductCategoryDto, ProductCategory>();
        CreateMap<GetProductCategoryDTO, ProductCategory>();
        
        CreateMap<Product, GetProductDto>()
            .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
        CreateMap<Image, GetImageDto>();
        
        CreateMap<PostCartItemsDto, CartItem>();
        CreateMap<CartItem, GetCartItemsDto>();
        
        CreateMap<UserAdress, GetUserAdressDto>();
        CreateMap<PostUserAdressDto, UserAdress>();
        CreateMap<PostUserAdressDto, OrderAdress>();
        
        CreateMap<PostPutUserPaymentDto, OrderPayment>();
        CreateMap<OrderAdress, GetUserAdressDto>();
        CreateMap<OrderPayment, GetUserPaymentDto>();

        CreateMap<UserPayment, GetUserPaymentDto>();
        CreateMap<PostPutUserPaymentDto, UserPayment>();
        CreateMap<PutUserAdressDto, UserAdress>();
        
        CreateMap<OrderDetail, GetOrdersDto>();
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(dest => dest.Product,
                opt => opt.MapFrom(src => src.Product));
    }
}