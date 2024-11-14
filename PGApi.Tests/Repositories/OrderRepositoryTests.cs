using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PGApi.Infrastructure.SqlServer;
using PGApi.Infrastructure.SqlServer.Repositories;
using PGApi.PGApi.Domain.Entities;
using PGApi.PGApi.Infrastructure.Repositories;
using Xunit;

namespace PGApi.Tests.Repositories
{
    /// <summary>
    /// Classe de testes para o repositório de pedidos (OrderRepository).
    /// Verifica a funcionalidade de adicionar e recuperar pedidos usando uma base de dados em memória.
    /// </summary>
    public class OrderRepositoryTests
    {
        private readonly SqlServerDbContext _dbContext;
        private readonly SqlServerOrderRepository _repository;

        /// <summary>
        /// Configuração inicial para os testes.
        /// Usa um banco de dados em memória para simular operações reais.
        /// </summary>
        public OrderRepositoryTests()
        {
            // Configura o DbContext para usar um banco de dados em memória com o nome "TestDatabase".
            var options = new DbContextOptionsBuilder<SqlServerDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _dbContext = new SqlServerDbContext(options);
            _repository = new SqlServerOrderRepository(_dbContext);
        }

        /// <summary>
        /// Teste para verificar se um pedido é adicionado corretamente ao banco de dados.
        /// </summary>
        [Fact]
        public async Task AddAsync_ShouldAddOrderToDatabase()
        {
            // Arrange
            // Cria um pedido com ID do produto 1 e quantidade 10.
            var order = new Order(1, 10);

            // Act
            // Adiciona o pedido ao repositório.
            await _repository.AddAsync(order);

            // Recupera todos os pedidos do banco de dados.
            var orders = await _repository.GetAllAsync();

            // Assert
            // Verifica se há apenas um pedido no banco.
            Assert.Single(orders);

            // Verifica se os dados do pedido armazenado correspondem aos dados esperados.
            Assert.Equal(order.ProductId, orders[0].ProductId);
            Assert.Equal(order.Quantity, orders[0].Quantity);
        }

        /// <summary>
        /// Teste para verificar se todos os pedidos armazenados no banco são recuperados corretamente.
        /// </summary>
        [Fact]
        public async Task GetAllAsync_ShouldReturnAllOrders()
        {
            // Arrange
            // Cria dois pedidos com diferentes IDs de produtos e quantidades.
            var order1 = new Order(1, 10);
            var order2 = new Order(2, 5);

            // Adiciona os dois pedidos ao repositório.
            await _repository.AddAsync(order1);
            await _repository.AddAsync(order2);

            // Act
            // Recupera todos os pedidos do banco de dados.
            var orders = await _repository.GetAllAsync();

            // Assert
            // Verifica se a quantidade total de pedidos recuperados é igual à quantidade adicionada.
            Assert.Equal(2, orders.Count);
        }
    }
}

