using BuildingBlocks.Core.Results;

namespace DiscountService.API.Features.GetCoupon;

public class GetCouponResponse
{
    public ServiceResult<CouponDto> Result { get; set; } = default!;
}

public class CouponDto
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
    public int UsedCount { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
