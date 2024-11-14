using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PGApi.Infrastructure.SqlServer;

namespace PGApi.PGApi.Infrastructure.SqlServer.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddSqlServerDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlServerDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SqlServer")));

            return services;
        }
    }
}
