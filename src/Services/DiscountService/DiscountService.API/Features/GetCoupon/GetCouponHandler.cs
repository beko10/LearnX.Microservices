using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Features.GetCoupon;

public class GetCouponHandler(
    DiscountDbContext dbContext,
    IDiscountBusinessRules businessRules,
    IMapper mapper
) : IRequestHandler<GetCouponRequest, GetCouponResponse>
{
    public async Task<GetCouponResponse> Handle(GetCouponRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => businessRules.CheckCouponExists(request.Id)
        );

        if (businessRulesResult.IsFail)
        {
            return new GetCouponResponse
            {
                Result = ServiceResult<CouponDto>.ErrorAsNotFound(
                    businessRulesResult.FailProblemDetails?.Detail ?? "Coupon not found.")
            };
        }

        var coupon = await dbContext.Coupons.FirstAsync(c => c.Id == request.Id, cancellationToken);
        var couponDto = mapper.Map<CouponDto>(coupon);

        return new GetCouponResponse
        {
            Result = ServiceResult<CouponDto>.SuccessAsOk(couponDto)
        };
    }
}
