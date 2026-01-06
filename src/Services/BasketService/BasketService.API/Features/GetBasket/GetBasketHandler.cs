using AutoMapper;
using BasketService.API.Common.Entities;
using BasketService.API.Common.Redis;
using BuildingBlocks.Core.Results;
using MediatR;

namespace BasketService.API.Features.GetBasket;

public class GetBasketHandler(
    IRedisService redisService,
    IMapper mapper
) : IRequestHandler<GetBasketRequest, GetBasketResponse>
{
    private static string GetBasketKey(string userId) => $"basket:{userId}";

    public async Task<GetBasketResponse> Handle(GetBasketRequest request, CancellationToken cancellationToken)
    {
        var basketKey = GetBasketKey(request.UserId);
        var basket = await redisService.GetAsync<Basket>(basketKey, cancellationToken);

        if (basket == null)
        {
            return new GetBasketResponse
            {
                Result = ServiceResult<BasketDto>.SuccessAsOk(new BasketDto
                {
                    UserId = request.UserId,
                    Items = [],
                    TotalPrice = 0,
                    CreatedAt = DateTime.UtcNow
                })
            };
        }

        var basketDto = mapper.Map<BasketDto>(basket);

        return new GetBasketResponse
        {
            Result = ServiceResult<BasketDto>.SuccessAsOk(basketDto)
        };
    }
}
