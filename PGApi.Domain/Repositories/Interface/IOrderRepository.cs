
using PGApi.PGApi.Domain.Entities;

namespace PGApi.PGApi.Infrastructure.Repositories
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
    }
}
