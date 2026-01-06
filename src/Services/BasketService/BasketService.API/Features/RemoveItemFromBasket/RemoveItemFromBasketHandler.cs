using BasketService.API.Common.Redis;
using BasketService.API.Common.Rules;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using MediatR;

namespace BasketService.API.Features.RemoveItemFromBasket;

public class RemoveItemFromBasketHandler(
    IRedisService redisService,
    IBasketBusinessRules basketBusinessRules
) : IRequestHandler<RemoveItemFromBasketRequest, RemoveItemFromBasketResponse>
{
    private static string GetBasketKey(string userId) => $"basket:{userId}";

    public async Task<RemoveItemFromBasketResponse> Handle(RemoveItemFromBasketRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => basketBusinessRules.CheckBasketExists(request.UserId),
            () => basketBusinessRules.CheckItemExistsInBasket(request.UserId, request.CourseId)
        );

        if (businessRulesResult.IsFail)
        {
            return new RemoveItemFromBasketResponse { Result = businessRulesResult };
        }

        var basketKey = GetBasketKey(request.UserId);
        var basket = await redisService.GetAsync<Common.Entities.Basket>(basketKey, cancellationToken);

        var item = basket!.Items.First(x => x.CourseId == request.CourseId);
        basket.Items.Remove(item);
        basket.UpdatedAt = DateTime.UtcNow;

        await redisService.SetAsync(basketKey, basket, cancellationToken: cancellationToken);

        return new RemoveItemFromBasketResponse
        {
            Result = ServiceResult.SuccessAsOk("Item removed from basket successfully.")
        };
    }
}
