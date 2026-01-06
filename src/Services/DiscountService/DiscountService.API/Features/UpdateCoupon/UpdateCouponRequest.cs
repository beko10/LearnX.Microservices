using MediatR;

namespace DiscountService.API.Features.UpdateCoupon;

public class UpdateCouponRequest : IRequest<UpdateCouponResponse>
{
    public string Id { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string Description { get; set; } = default!;
    public decimal DiscountAmount { get; set; }
    public int DiscountPercentage { get; set; }
    public bool IsPercentage { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidUntil { get; set; }
    public int UsageLimit { get; set; }
    public bool IsActive { get; set; }
}
