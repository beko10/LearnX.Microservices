using BuildingBlocks.Core.Results;

namespace BasketService.API.Common.Rules;

public interface IBasketBusinessRules
{
    // ============== EXISTENCE CHECKS ==============
    Task<ServiceResult> CheckBasketExists(string userId);
    Task<ServiceResult> CheckItemExistsInBasket(string userId, string courseId);
    
    // ============== QUANTITY CONSTRAINTS ==============
    ServiceResult CheckQuantityIsValid(int quantity);
    ServiceResult CheckMaxItemsNotReached(int currentItemCount);
    
    // ============== PRICE VALIDATION ==============
    ServiceResult CheckPriceIsValid(decimal price);
}
