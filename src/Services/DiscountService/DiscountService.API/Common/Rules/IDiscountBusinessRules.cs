using BuildingBlocks.Core.Results;
using DiscountService.API.Common.Entities;

namespace DiscountService.API.Common.Rules;

public interface IDiscountBusinessRules
{
    // ============== EXISTENCE CHECKS ==============
    Task<ServiceResult> CheckCouponExists(string couponId);
    Task<ServiceResult> CheckCouponCodeExists(string code);
    Task<ServiceResult> CheckCouponCodeIsUnique(string code);
    Task<ServiceResult> CheckCouponCodeIsUniqueExceptCurrent(string code, string currentId);

    // ============== VALIDATION ==============
    ServiceResult CheckDiscountValuesAreValid(decimal amount, int percentage, bool isPercentage);
    ServiceResult CheckDateRangeIsValid(DateTime validFrom, DateTime validUntil);
    Task<ServiceResult> CheckCouponIsUsable(string couponId);
}
