using AutoMapper;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using DiscountService.API.Common.Entities;
using DiscountService.API.Common.MongoDB;
using DiscountService.API.Common.Rules;
using MediatR;

namespace DiscountService.API.Features.CreateCoupon;

public class CreateCouponHandler(
    DiscountDbContext dbContext,
    IDiscountBusinessRules businessRules,
    IMapper mapper
) : IRequestHandler<CreateCouponRequest, CreateCouponResponse>
{
    public async Task<CreateCouponResponse> Handle(CreateCouponRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => businessRules.CheckCouponCodeIsUnique(request.Code),
            () => Task.FromResult(businessRules.CheckDiscountValuesAreValid(
                request.DiscountAmount, request.DiscountPercentage, request.IsPercentage)),
            () => Task.FromResult(businessRules.CheckDateRangeIsValid(request.ValidFrom, request.ValidUntil))
        );

        if (businessRulesResult.IsFail)
        {
            return new CreateCouponResponse
            {
                Result = ServiceResult<string>.ErrorAsCustom(
                    businessRulesResult.StatusCode,
                    businessRulesResult.FailProblemDetails?.Title ?? "Validation Error",
                    businessRulesResult.FailProblemDetails?.Detail)
            };
        }

        var coupon = mapper.Map<Coupon>(request);

        await dbContext.Coupons.AddAsync(coupon, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new CreateCouponResponse
        {
            Result = ServiceResult<string>.SuccessAsCreated(coupon.Id)
        };
    }
}
