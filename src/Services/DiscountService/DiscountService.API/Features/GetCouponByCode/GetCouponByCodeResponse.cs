using BuildingBlocks.Core.Results;
using DiscountService.API.Features.GetCoupon;

namespace DiscountService.API.Features.GetCouponByCode;

public class GetCouponByCodeResponse
{
    public ServiceResult<CouponDto> Result { get; set; } = default!;
}
