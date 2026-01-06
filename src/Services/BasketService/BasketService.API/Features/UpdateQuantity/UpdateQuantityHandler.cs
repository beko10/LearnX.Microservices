using BasketService.API.Common.Entities;
using BasketService.API.Common.Redis;
using BasketService.API.Common.Rules;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using MediatR;

namespace BasketService.API.Features.UpdateQuantity;

public class UpdateQuantityHandler(
    IRedisService redisService,
    IBasketBusinessRules basketBusinessRules
) : IRequestHandler<UpdateQuantityRequest, UpdateQuantityResponse>
{
    private static string GetBasketKey(string userId) => $"basket:{userId}";

    public async Task<UpdateQuantityResponse> Handle(UpdateQuantityRequest request, CancellationToken cancellationToken)
    {
        // Business Rules Check - All rules in one place
        var businessRulesResult = await BusinessRuleEngine.RunAsync(
            () => basketBusinessRules.CheckBasketExists(request.UserId),
            () => basketBusinessRules.CheckItemExistsInBasket(request.UserId, request.CourseId),
            () => Task.FromResult(basketBusinessRules.CheckQuantityIsValid(request.Quantity))
        );

        if (businessRulesResult.IsFail)
        {
            return new UpdateQuantityResponse { Result = businessRulesResult };
        }

        var basketKey = GetBasketKey(request.UserId);
        var basket = await redisService.GetAsync<Basket>(basketKey, cancellationToken);

        var item = basket!.Items.First(x => x.CourseId == request.CourseId);

        if (request.Quantity <= 0)
        {
            // Miktar 0 veya negatifse ürünü sil
            basket.Items.Remove(item);
        }
        else
        {
            item.Quantity = request.Quantity;
        }

        basket.UpdatedAt = DateTime.UtcNow;

        await redisService.SetAsync(basketKey, basket, cancellationToken: cancellationToken);

        return new UpdateQuantityResponse
        {
            Result = ServiceResult.SuccessAsOk("Quantity updated successfully.")
        };
    }
}
