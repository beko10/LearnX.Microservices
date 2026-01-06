using AutoMapper;
using DiscountService.API.Common.Entities;
using DiscountService.API.Features.CreateCoupon;
using DiscountService.API.Features.GetCoupon;
using DiscountService.API.Features.UpdateCoupon;

namespace DiscountService.API.Common.Mappings;

public class CouponMappingProfile : Profile
{
    public CouponMappingProfile()
    {
        // Entity -> DTO
        CreateMap<Coupon, CouponDto>();

        // Request -> Entity
        CreateMap<CreateCouponRequest, Coupon>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.UsedCount, opt => opt.Ignore())
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(_ => true))
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .AfterMap((src, dest) => dest.Code = src.Code.ToUpper());

        CreateMap<UpdateCouponRequest, Coupon>()
            .ForMember(dest => dest.UsedCount, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .AfterMap((src, dest) => dest.Code = src.Code.ToUpper());
    }
}
