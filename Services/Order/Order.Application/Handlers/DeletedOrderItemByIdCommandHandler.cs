using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Commands;
using Order.Application.Dtos;
using Order.Application.Mapping;
using Order.Infrastructure;
using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Handlers
{
    public class DeletedOrderItemByIdCommandHandler : IRequestHandler<DeleteOrderItemByIdCommand, Response<DeletedOrderItemDto>>
    {
        private readonly OrderDbContext _context;
        public DeletedOrderItemByIdCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<Response<DeletedOrderItemDto>> Handle(DeleteOrderItemByIdCommand request, CancellationToken cancellationToken)
        {
            
            var deletedOrderItem = await _context.OrdersItems.Where(x=>x.Id==request.OrderItemId).FirstAsync();

            if (deletedOrderItem.Id != 0)
            {
                _context.OrdersItems.Remove(deletedOrderItem);
                await _context.SaveChangesAsync();
                return Response<DeletedOrderItemDto>.Success(new DeletedOrderItemDto { OrderItemId = deletedOrderItem.Id },200);
            }
            return Response<DeletedOrderItemDto>.Fail("OrderItem not found",404);



        }
    }
}
