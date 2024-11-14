using MediatR;
using PGApi.PGApi.Domain.Entities;

namespace PGApi.PGApi.Application.Queries
{
    public class GetAllOrdersQuery : IRequest<List<Order>>
    {
    }
}
