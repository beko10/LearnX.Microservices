using MediatR;

namespace DiscountService.API.Features.DeleteCoupon;

public class DeleteCouponRequest : IRequest<DeleteCouponResponse>
{
    public string Id { get; set; } = default!;
}
