using Microsoft.EntityFrameworkCore;
using PGApi.PGApi.Domain.Entities;

namespace PGApi.PGApi.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    }
}
