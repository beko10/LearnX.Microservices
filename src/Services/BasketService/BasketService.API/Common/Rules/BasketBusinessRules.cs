using BasketService.API.Common.Entities;
using BasketService.API.Common.Redis;
using BuildingBlocks.Core.Results;

namespace BasketService.API.Common.Rules;

public class BasketBusinessRules(IRedisService redisService) : IBasketBusinessRules
{
    private const int MaxItemsInBasket = 20;
    private const int MaxQuantityPerItem = 10;
    private const decimal MinPrice = 0.01m;
    private const decimal MaxPrice = 100000m;

    private static string GetBasketKey(string userId) => $"basket:{userId}";

    // ============== EXISTENCE CHECKS ==============

    public async Task<ServiceResult> CheckBasketExists(string userId)
    {
        var basket = await redisService.GetAsync<Basket>(GetBasketKey(userId));
        
        return basket != null
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound($"Basket not found for user: {userId}");
    }

    public async Task<ServiceResult> CheckItemExistsInBasket(string userId, string courseId)
    {
        var basket = await redisService.GetAsync<Basket>(GetBasketKey(userId));
        
        if (basket == null)
            return ServiceResult.ErrorAsNotFound($"Basket not found for user: {userId}");

        var itemExists = basket.Items.Any(x => x.CourseId == courseId);
        
        return itemExists
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.ErrorAsNotFound($"Item with CourseId '{courseId}' not found in basket.");
    }

    // ============== QUANTITY CONSTRAINTS ==============

    public ServiceResult CheckQuantityIsValid(int quantity)
    {
        if (quantity < 0)
            return ServiceResult.Fail("Quantity cannot be negative.");

        if (quantity > MaxQuantityPerItem)
            return ServiceResult.Fail($"Quantity cannot exceed {MaxQuantityPerItem} per item.");

        return ServiceResult.SuccessAsNoContent();
    }

    public ServiceResult CheckMaxItemsNotReached(int currentItemCount)
    {
        return currentItemCount < MaxItemsInBasket
            ? ServiceResult.SuccessAsNoContent()
            : ServiceResult.Fail($"Basket cannot contain more than {MaxItemsInBasket} different items.");
    }

    // ============== PRICE VALIDATION ==============

    public ServiceResult CheckPriceIsValid(decimal price)
    {
        if (price < MinPrice)
            return ServiceResult.Fail($"Price must be at least {MinPrice}.");

        if (price > MaxPrice)
            return ServiceResult.Fail($"Price cannot exceed {MaxPrice}.");

        return ServiceResult.SuccessAsNoContent();
    }
}
