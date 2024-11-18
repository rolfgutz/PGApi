using MediatR; // Para IRequest
using System.Collections.Generic; // Para List<T>
using PGApi.Application.DTOs; // Para OrderDto
using PGApi.Application.Utils;

namespace PGApi.Application.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<Result>
    {
    }
}
