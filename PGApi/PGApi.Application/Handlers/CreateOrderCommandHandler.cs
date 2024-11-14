using MediatR;
using PGApi.PGApi.Application.Commands;
using PGApi.PGApi.Domain.Entities;
using PGApi.PGApi.Infrastructure.Repositories;
using PGApi.PGApi.Shared;

namespace PGApi.PGApi.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.OrderDto.ProductId, request.OrderDto.Quantity);
            await _orderRepository.AddAsync(order);
            return Result.Success(order.Id);
        }
    }
}
