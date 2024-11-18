using MediatR;
using PGApi.Application.Commands;
using PGApi.Application.Utils;
using PGApi.Domain.Repositories.Interface;
using PGApi.PGApi.Domain.Entities;

namespace PGApi.Application.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Validação simples: quantidade deve ser maior que 0
                if (request.OrderDto.Quantity <= 0)
                    return Result.Failure("Quantidade deve ser maior que zero.");

                // Criação do pedido (domínio)
                var order = new Order(request.OrderDto.ProductId, request.OrderDto.Quantity);

                // Adicionar o pedido no repositório (persistir no banco de dados)
                await _orderRepository.AddAsync(order);

                // Retorna sucesso com o Id do pedido criado
                return Result.Success();
            }
            catch (Exception ex)
            {
                // Retorna erro se ocorrer uma exceção
                return Result.Failure($"Erro ao criar o pedido: {ex.Message}");
            }
        }
    }
}
