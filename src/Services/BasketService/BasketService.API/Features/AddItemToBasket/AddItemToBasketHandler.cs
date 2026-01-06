using AutoMapper;
using BasketService.API.Common.Entities;
using BasketService.API.Common.Redis;
using BasketService.API.Common.Rules;
using BuildingBlocks.Core.BusinessRuleEngine;
using BuildingBlocks.Core.Results;
using MediatR;

namespace BasketService.API.Features.AddItemToBasket;

public class AddItemToBasketHandler(
    IRedisService redisService,
    IBasketBusinessRules basketBusinessRules,
    IMapper mapper
) : IRequestHandler<AddItemToBasketRequest, AddItemToBasketResponse>
{
    private static string GetBasketKey(string userId) => $"basket:{userId}";

    public async Task<AddItemToBasketResponse> Handle(AddItemToBasketRequest request, CancellationToken cancellationToken)
    {
        // Phase 1: Input validation (sync rules)
        var inputValidationResult = BusinessRuleEngine.Run(
            basketBusinessRules.CheckPriceIsValid(request.Price),
            basketBusinessRules.CheckQuantityIsValid(request.Quantity)
        );

        if (inputValidationResult.IsFail)
        {
            return new AddItemToBasketResponse { Result = inputValidationResult };
        }

        var basketKey = GetBasketKey(request.UserId);
        
        // Mevcut sepeti al veya yeni oluştur
        var basket = await redisService.GetAsync<Basket>(basketKey, cancellationToken) 
            ?? new Basket { UserId = request.UserId };

        var existingItem = basket.Items.FirstOrDefault(x => x.CourseId == request.CourseId);
        var newTotalQuantity = existingItem != null 
            ? existingItem.Quantity + request.Quantity 
            : request.Quantity;

        // Phase 2: State-dependent validation (after data loaded)
        var stateValidationResult = BusinessRuleEngine.Run(
            existingItem == null 
                ? basketBusinessRules.CheckMaxItemsNotReached(basket.Items.Count) 
                : ServiceResult.SuccessAsNoContent(),
            basketBusinessRules.CheckQuantityIsValid(newTotalQuantity)
        );

        if (stateValidationResult.IsFail)
        {
            return new AddItemToBasketResponse { Result = stateValidationResult };
        }

        // Apply changes
        if (existingItem != null)
        {
            existingItem.Quantity = newTotalQuantity;
        }
        else
        {
            var newItem = mapper.Map<BasketItem>(request);
            basket.Items.Add(newItem);
        }

        basket.UpdatedAt = DateTime.UtcNow;

        // Redis'e kaydet
        await redisService.SetAsync(basketKey, basket, cancellationToken: cancellationToken);

        return new AddItemToBasketResponse
        {
            Result = ServiceResult.SuccessAsOk("Item added to basket successfully.")
        };
    }
}
