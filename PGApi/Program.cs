

using Microsoft.EntityFrameworkCore;
using PGApi.PGApi.Infrastructure.Repositories;
using PGApi.Infrastructure.SqlServer.Repositories;
using PGApi.Infrastructure.SqlServer;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados SQL Server
var connectionString = builder.Configuration.GetConnectionString("SqlServer");

// Adicionando o DbContext do SQL Server
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(connectionString));

//Adicionar o repositório (IOrderRepository -> SqlServerOrderRepository)
builder.Services.AddScoped<IOrderRepository, SqlServerOrderRepository>();

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure o pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
