using MediatR;
using PGApi.Application.DTOs;
using PGApi.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Application.Commands.UpdateOrder
{
    public class UpdateOrderCommand : IRequest<Result>
    {
        public int OrderId { get; set; }

        public UpdateOrderDto OrderDto { get; set; }

        public UpdateOrderCommand(int orderId, UpdateOrderDto orderDto)
        {
            OrderId = orderId;
            OrderDto = orderDto;
        }
    }
}
