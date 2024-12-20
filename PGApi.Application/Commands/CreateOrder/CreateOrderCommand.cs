﻿using MediatR;
using PGApi.Application.DTOs;
using PGApi.Application.Utils;

namespace PGApi.Application.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<Result>
    {
        public CreateOrderDto OrderDto { get; }

        public CreateOrderCommand(CreateOrderDto orderDto)
        {
            OrderDto = orderDto;
        }
    }
}
