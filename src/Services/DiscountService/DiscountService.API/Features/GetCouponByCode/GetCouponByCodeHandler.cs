using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using DiscountService.API.Features.GetCoupon;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DiscountService.API.Features.GetCouponByCode;

public class GetCouponByCodeHandler(
    DiscountDbContext dbContext,
    IDiscountBusinessRules businessRules,
    IMapper mapper
) : IRequestHandler<GetCouponByCodeRequest, GetCouponByCodeResponse>
{
    public async Task<GetCouponByCodeResponse> Handle(GetCouponByCodeRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => businessRules.CheckCouponCodeExists(request.Code)
        );

        if (businessRulesResult.IsFail)
        {
            return new GetCouponByCodeResponse
            {
                Result = ServiceResult<CouponDto>.ErrorAsNotFound(
                    businessRulesResult.FailProblemDetails?.Detail ?? "Coupon not found.")
            };
        }

        var coupon = await dbContext.Coupons
            .FirstAsync(c => c.Code.ToLower() == request.Code.ToLower(), cancellationToken);
        
        var couponDto = mapper.Map<CouponDto>(coupon);

        return new GetCouponByCodeResponse
        {
            Result = ServiceResult<CouponDto>.SuccessAsOk(couponDto)
        };
    }
}
