using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PGApi.Infrastructure.SqlServer
{
    public class SqlServerDbContextFactory : IDesignTimeDbContextFactory<SqlServerDbContext>
    {
        public SqlServerDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SqlServerDbContext>();

            // Configura a conexão com o banco de dados SQL Server
            optionsBuilder.UseSqlServer("Server=localhost;Database=PGApiDb;Trusted_Connection=True;");

            return new SqlServerDbContext(optionsBuilder.Options);
        }
    }
}
