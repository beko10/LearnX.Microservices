using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Features.DeleteCoupon;

public class DeleteCouponHandler(
    DiscountDbContext dbContext,
    IDiscountBusinessRules businessRules
) : IRequestHandler<DeleteCouponRequest, DeleteCouponResponse>
{
    public async Task<DeleteCouponResponse> Handle(DeleteCouponRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => businessRules.CheckCouponExists(request.Id)
        );

        if (businessRulesResult.IsFail)
        {
            return new DeleteCouponResponse { Result = businessRulesResult };
        }

        var coupon = await dbContext.Coupons.FirstAsync(c => c.Id == request.Id, cancellationToken);
        dbContext.Coupons.Remove(coupon);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteCouponResponse
        {
            Result = ServiceResult.SuccessAsOk("Coupon deleted successfully.")
        };
    }
}
