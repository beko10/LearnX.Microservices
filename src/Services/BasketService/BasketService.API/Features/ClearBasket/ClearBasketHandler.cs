using BasketService.API.Common.Redis;
using BasketService.API.Common.Rules;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using MediatR;

namespace BasketService.API.Features.ClearBasket;

public class ClearBasketHandler(
    IRedisService redisService,
    IBasketBusinessRules basketBusinessRules
) : IRequestHandler<ClearBasketRequest, ClearBasketResponse>
{
    private static string GetBasketKey(string userId) => $"basket:{userId}";

    public async Task<ClearBasketResponse> Handle(ClearBasketRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => basketBusinessRules.CheckBasketExists(request.UserId)
        );

        if (businessRulesResult.IsFail)
        {
            return new ClearBasketResponse { Result = businessRulesResult };
        }

        var basketKey = GetBasketKey(request.UserId);
        await redisService.RemoveAsync(basketKey, cancellationToken);

        return new ClearBasketResponse
        {
            Result = ServiceResult.SuccessAsOk("Basket cleared successfully.")
        };
    }
}
