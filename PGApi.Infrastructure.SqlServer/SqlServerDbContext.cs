using Microsoft.EntityFrameworkCore;
using PGApi.PGApi.Domain.Entities;

namespace PGApi.Infrastructure.SqlServer
{
    public class SqlServerDbContext : DbContext
    {
        public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options)
         : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
