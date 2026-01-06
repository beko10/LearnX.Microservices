using BuildingBlocks.Core.Results;
using DiscountService.API.Common.Entities;
using DiscountService.API.Common.MongoDB;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Common.Rules;

public class DiscountBusinessRules(DiscountDbContext dbContext) : IDiscountBusinessRules
{
    // ============== EXISTENCE CHECKS ==============

    public async Task<ServiceResult> CheckCouponExists(string couponId)
    {
        var exists = await dbContext.Coupons.AnyAsync(c => c.Id == couponId);

        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound($"Coupon with ID '{couponId}' not found.");
    }

    public async Task<ServiceResult> CheckCouponCodeExists(string code)
    {
        var exists = await dbContext.Coupons.AnyAsync(c => c.Code == code);

        return exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound($"Coupon with code '{code}' not found.");
    }

    public async Task<ServiceResult> CheckCouponCodeIsUnique(string code)
    {
        var exists = await dbContext.Coupons.AnyAsync(c => c.Code.ToLower() == code.ToLower());

        return !exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail($"A coupon with code '{code}' already exists.");
    }

    public async Task<ServiceResult> CheckCouponCodeIsUniqueExceptCurrent(string code, string currentId)
    {
        var exists = await dbContext.Coupons.AnyAsync(c => 
            c.Code.ToLower() == code.ToLower() && c.Id != currentId);

        return !exists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail($"A coupon with code '{code}' already exists.");
    }

    // ============== VALIDATION ==============

    public ServiceResult CheckDiscountValuesAreValid(decimal amount, int percentage, bool isPercentage)
    {
        if (isPercentage)
        {
            if (percentage <= 0 || percentage > 100)
                return ServiceResult.Fail("Discount percentage must be between 1 and 100.");
        }
        else
        {
            if (amount <= 0)
                return ServiceResult.Fail("Discount amount must be greater than 0.");
        }

        return ServiceResult.SuccessAsNoContent();
    }

    public ServiceResult CheckDateRangeIsValid(DateTime validFrom, DateTime validUntil)
    {
        if (validFrom >= validUntil)
            return ServiceResult.Fail("Valid from date must be before valid until date.");

        return ServiceResult.SuccessAsNoContent();
    }

    public async Task<ServiceResult> CheckCouponIsUsable(string couponId)
    {
        var coupon = await dbContext.Coupons.FirstOrDefaultAsync(c => c.Id == couponId);

        if (coupon == null)
            return ServiceResult.ErrorAsNotFound($"Coupon with ID '{couponId}' not found.");

        if (!coupon.IsActive)
            return ServiceResult.Fail("This coupon is not active.");

        if (DateTime.UtcNow < coupon.ValidFrom)
            return ServiceResult.Fail("This coupon is not yet valid.");

        if (DateTime.UtcNow > coupon.ValidUntil)
            return ServiceResult.Fail("This coupon has expired.");

        if (coupon.UsageLimit > 0 && coupon.UsedCount >= coupon.UsageLimit)
            return ServiceResult.Fail("This coupon has reached its usage limit.");

        return ServiceResult.SuccessAsNoContent();
    }
}
