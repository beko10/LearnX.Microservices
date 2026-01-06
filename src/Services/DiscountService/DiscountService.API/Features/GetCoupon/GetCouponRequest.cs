using MediatR;

namespace DiscountService.API.Features.GetCoupon;

public class GetCouponRequest : IRequest<GetCouponResponse>
{
    public string Id { get; set; } = default!;
}
