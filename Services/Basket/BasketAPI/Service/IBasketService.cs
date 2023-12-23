using CampExplorer.Services.Basket.Dtos;
using Core.Dtos;

namespace BasketAPI.Service
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basket);
        Task<Response<bool>> Delete(string userId);
    }
}
