using BuildingBlocks.Core.Results;

namespace DiscountService.API.Features.CreateCoupon;

public class CreateCouponResponse
{
    public ServiceResult<string> Result { get; set; } = default!;
}
