using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Features.UpdateCoupon;

public class UpdateCouponHandler(
    DiscountDbContext dbContext,
    IDiscountBusinessRules businessRules,
    IMapper mapper
) : IRequestHandler<UpdateCouponRequest, UpdateCouponResponse>
{
    public async Task<UpdateCouponResponse> Handle(UpdateCouponRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => businessRules.CheckCouponExists(request.Id),
            () => businessRules.CheckCouponCodeIsUniqueExceptCurrent(request.Code, request.Id),
            () => Task.FromResult(businessRules.CheckDiscountValuesAreValid(
                request.DiscountAmount, request.DiscountPercentage, request.IsPercentage)),
            () => Task.FromResult(businessRules.CheckDateRangeIsValid(request.ValidFrom, request.ValidUntil))
        );

        if (businessRulesResult.IsFail)
        {
            return new UpdateCouponResponse { Result = businessRulesResult };
        }

        var coupon = await dbContext.Coupons.FirstAsync(c => c.Id == request.Id, cancellationToken);

        // Map request to existing entity (preserves Id, CreatedAt, UsedCount)
        mapper.Map(request, coupon);

        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateCouponResponse
        {
            Result = ServiceResult.SuccessAsOk("Coupon updated successfully.")
        };
    }
}
