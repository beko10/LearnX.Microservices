using MediatR;

namespace BasketService.API.Features.ClearBasket;

public class ClearBasketRequest : IRequest<ClearBasketResponse>
{
    public string UserId { get; set; } = default!;
}
