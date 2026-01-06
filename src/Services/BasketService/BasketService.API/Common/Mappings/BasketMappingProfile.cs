using AutoMapper;
using BasketService.API.Common.Entities;
using BasketService.API.Features.AddItemToBasket;
using BasketService.API.Features.GetBasket;

namespace BasketService.API.Common.Mappings;

public class BasketMappingProfile : Profile
{
    public BasketMappingProfile()
    {
        // Entity -> DTO
        CreateMap<Basket, BasketDto>();
        CreateMap<BasketItem, BasketItemDto>();

        // Request -> Entity
        CreateMap<AddItemToBasketRequest, BasketItem>()
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
    }
}
