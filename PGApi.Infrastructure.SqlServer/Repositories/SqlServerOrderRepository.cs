using Microsoft.EntityFrameworkCore;
using PGApi.Domain.Repositories.Interface;
using PGApi.PGApi.Domain.Entities;

namespace PGApi.Infrastructure.SqlServer.Repositories
{
    public class SqlServerOrderRepository : IOrderRepository
    {
        private readonly SqlServerDbContext _context;

        public SqlServerOrderRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order); 
            await _context.SaveChangesAsync(); 
        }
    }
}
