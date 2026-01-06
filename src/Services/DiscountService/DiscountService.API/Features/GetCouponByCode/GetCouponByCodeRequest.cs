using MediatR;

namespace DiscountService.API.Features.GetCouponByCode;

public class GetCouponByCodeRequest : IRequest<GetCouponByCodeResponse>
{
    public string Code { get; set; } = default!;
}
