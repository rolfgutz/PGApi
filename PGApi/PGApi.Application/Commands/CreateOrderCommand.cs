using MediatR;
using PGApi.PGApi.Application.DTOs;
using PGApi.PGApi.Shared;

namespace PGApi.PGApi.Application.Commands

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
