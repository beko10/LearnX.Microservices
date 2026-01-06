namespace DiscountService.API.Common.Entities;

public class Coupon
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
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public decimal CalculateDiscount(decimal originalPrice)
    {
        if (!IsActive || DateTime.UtcNow < ValidFrom || DateTime.UtcNow > ValidUntil)
            return 0;

        if (UsageLimit > 0 && UsedCount >= UsageLimit)
            return 0;

        return IsPercentage
            ? originalPrice * DiscountPercentage / 100
            : Math.Min(DiscountAmount, originalPrice);
    }
}
