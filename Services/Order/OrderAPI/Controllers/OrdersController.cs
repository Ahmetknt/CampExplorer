using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Commands;
using Order.Application.Handlers;
using Order.Application.Queries;
using Core.ControllerBases;
using Core.Services;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : CustomBaseController
    {
        private readonly IMediator _mediator;
        private readonly ICoreIdentityService _identityService;

        public OrdersController(IMediator mediator, ICoreIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _identityService.GetUserId});
            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrder(CreateOrderCommand createOrderCommand)
        {
            var response = await _mediator.Send(createOrderCommand);
            return CreateActionResultInstance(response);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(DeleteOrderItemByIdCommand deleteOrderItemByIdCommand)
        {
            var response = await _mediator.Send(deleteOrderItemByIdCommand);
            return CreateActionResultInstance(response);
        }
    }
}
