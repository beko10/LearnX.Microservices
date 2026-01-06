using MediatR;

namespace BasketService.API.Features.GetBasket;

public class GetBasketRequest : IRequest<GetBasketResponse>
{
    public string UserId { get; set; } = default!;
}
