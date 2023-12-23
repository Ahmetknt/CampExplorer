using BasketAPI.Model;
using CampExplorer.Services.Basket.Dtos;
using Core.Dtos;
using Core.Services;
using System.Text.Json;

namespace BasketAPI.Service
{
    public class BasketService : IBasketService
    {

        private readonly RedisService _redisService;
        private readonly ICoreIdentityService _coreIdentityService;
        public BasketService(RedisService redisService,ICoreIdentityService coreIdentityService)
        {
            _redisService = redisService;
            _coreIdentityService = coreIdentityService;
        }

        public async Task<Response<bool>> Delete(string userId)
        {
            var status = await _redisService.GetDb().KeyDeleteAsync(userId);
            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket not found", 404);
        }

        public async Task<Response<BasketDto>> GetBasket(string userId)
        {
            var existBasket = await _redisService.GetDb().StringGetAsync(userId);

            if (String.IsNullOrEmpty(existBasket))
            {
                return Response<BasketDto>.Fail("Basket not found", 404);
            }

            return Response<BasketDto>.Success(JsonSerializer.Deserialize<BasketDto>(existBasket), 200);
        }

        public async Task<Response<bool>> SaveOrUpdate(BasketDto basketDto)
        {
            var status = await _redisService.GetDb().StringSetAsync(_coreIdentityService.GetUserId, JsonSerializer.Serialize(basketDto));

            return status ? Response<bool>.Success(204) : Response<bool>.Fail("Basket could not update or save", 500);
        }
    }
}
