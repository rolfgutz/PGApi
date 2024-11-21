using MediatR;
using PGApi.Application.Utils;
using PGApi.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Application.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public UpdateOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Busca o pedido no repositório
                var existingOrder = await _orderRepository.GetByIdAsync(request.OrderDto.ProductId);

                if (existingOrder == null)
                    return Result.Failure("Pedido não encontrado.");

                // Atualiza as propriedades utilizando o método Update
                existingOrder.Update(request.OrderDto.Quantity);

                // Salva as alterações no banco
                await _orderRepository.UpdateAsync(existingOrder);

                return Result.Success();
            }
            catch (Exception ex)
            {
                return Result.Failure($"Erro ao atualizar o pedido: {ex.Message}");
            }
        }
    }
}
