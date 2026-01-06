using MediatR;

namespace BasketService.API.Features.RemoveItemFromBasket;

public class RemoveItemFromBasketRequest : IRequest<RemoveItemFromBasketResponse>
{
    public string UserId { get; set; } = default!;
    public string CourseId { get; set; } = default!;
}
