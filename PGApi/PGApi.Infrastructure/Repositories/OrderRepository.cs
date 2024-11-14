using Microsoft.EntityFrameworkCore;
using PGApi.PGApi.Domain.Entities;
using PGApi.PGApi.Infrastructure.Data;

namespace PGApi.PGApi.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order); // Adiciona o pedido
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }
       
    }
}
