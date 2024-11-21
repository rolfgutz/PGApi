using MediatR;
using PGApi.Application.DTOs;
using PGApi.Application.Utils;
using PGApi.Domain.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, Result>
    {
        private readonly IOrderRepository _orderRepository;

        public GetAllOrdersQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Result> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Recupera todos os pedidos do repositório
                var orders = await _orderRepository.GetAllAsync();

                // Mapeia as entidades de domínio para DTOs
                var orderDtos = orders.ConvertAll(order => new OrderDto
                {
                    Id = order.Id,
                    ProductId = order.ProductId,
                    Quantity = order.Quantity,
                    // Mapear outras propriedades do Order conforme necessário
                });

                // Retorna um resultado com sucesso e a lista de pedidos (passando os dados na resposta)
                return Result.Success(orderDtos); // Passando os dados no Result
            }
            catch (Exception ex)
            {
                // Retorna falha em caso de erro
                return Result.Failure($"Erro ao obter os pedidos: {ex.Message}");
            }

        }
    }
}

