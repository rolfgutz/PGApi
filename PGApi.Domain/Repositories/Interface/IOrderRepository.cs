
using PGApi.PGApi.Domain.Entities;

namespace PGApi.Domain.Repositories.Interface
{
    public interface IOrderRepository
    {
        Task AddAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<Order> GetByIdAsync(int id);
        Task UpdateAsync(Order order); 
    }
}
