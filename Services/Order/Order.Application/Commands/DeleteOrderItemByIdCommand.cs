using MediatR;
using Order.Application.Dtos;
using Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Application.Commands
{
    public class DeleteOrderItemByIdCommand :IRequest<Response<DeletedOrderItemDto>>
    {
        public int OrderItemId { get; set; }
    }
}
