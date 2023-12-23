using BasketAPI.Model;
using BasketAPI.Service;
using CampExplorer.Services.Basket.Dtos;
using Core.ControllerBases;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace BasketAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : CustomBaseController
    {
        private readonly IBasketService _basketService;
        private readonly ICoreIdentityService _identityService;

        public BasketsController(IBasketService basketService,ICoreIdentityService identityService)
        {
            _basketService = basketService;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var response = await _basketService.GetBasket(_identityService.GetUserId);
            return CreateActionResultInstance(response);

        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdate(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto);
            return CreateActionResultInstance(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket() { 
            var response = await _basketService.Delete(_identityService.GetUserId);
            return CreateActionResultInstance(response);
        }


    }
}
