using MediatR;
using PGApi.Application.Utils;

namespace PGApi.Application.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest<Result>
    {
        public int OrderId { get; }

        public DeleteOrderCommand(int orderId)
        {
            OrderId = orderId;
        }
    }
}
