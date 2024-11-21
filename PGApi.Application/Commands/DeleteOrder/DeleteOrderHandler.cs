using MediatR;
using PGApi.Application.Utils;
using PGApi.Domain.Repositories.Interface;

namespace PGApi.Application.Commands.DeleteOrder
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Verifica se o pedido existe
                var existingOrder = await _orderRepository.GetByIdAsync(request.OrderId);
                if (existingOrder == null)
                    return Result.Failure("Pedido não encontrado.");

                // Remove o pedido
                await _orderRepository.DeleteAsync(existingOrder);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Erro ao deletar o pedido: {ex.Message}");
            }
        }
    }
}
