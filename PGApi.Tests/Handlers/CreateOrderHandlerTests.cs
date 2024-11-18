using Moq;
using PGApi.Application.Commands.CreateOrder;
using PGApi.Application.DTOs;
using PGApi.Domain.Repositories.Interface;
using PGApi.PGApi.Domain.Entities;
using Xunit;

namespace PGApi.Tests.Handlers
{
    public class CreateOrderHandlerTests
    {
        // Declaração de um mock do repositório de pedidos
        private readonly Mock<IOrderRepository> _mockOrderRepository;

        // Declaração do handler que será testado (o manipulador do comando)
        private readonly CreateOrderHandler _handler;

        // O construtor da classe de testes, que inicializa os objetos necessários
        public CreateOrderHandlerTests()
        {
            // Criação do mock para o repositório de pedidos (não acessa o banco real)
            _mockOrderRepository = new Mock<IOrderRepository>();

            // Criação do handler que será testado, passando o repositório mockado
            _handler = new CreateOrderHandler(_mockOrderRepository.Object);
        }

        // Teste para verificar se o comando de criação do pedido retorna sucesso quando o pedido é criado corretamente
        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenOrderIsCreatedSuccessfully()
        {
            // Arrange: configurando os dados de entrada para o comando
            // Criando um objeto 'CreateOrderDto' que contém as informações necessárias para criar o pedido
            var orderDto = new CreateOrderDto
            {
                ProductId = 1, // Id do produto
                Quantity = 10 // Quantidade do produto
            };

            // Criando o comando de criação de pedido que será enviado para o handler
            var command = new CreateOrderCommand(orderDto);

            // Arrange: configurando o comportamento esperado para o mock do repositório
            // O repositório mockado não vai fazer nada, apenas "simula" que o pedido foi adicionado com sucesso
            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask); // Simula uma operação assíncrona bem-sucedida

            // Act: executando o comando, que chama o handler para processar a criação do pedido
            var result = await _handler.Handle(command, default); // O método Handle do handler é chamado

            // Assert: verificando o resultado do comando
            // Verifica se o resultado da operação foi bem-sucedido
            Assert.True(result.IsSuccess);

            // Verifica se o método AddAsync foi chamado exatamente uma vez, garantindo que o pedido foi adicionado
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        }

        // Teste para verificar se o comando retorna falha quando a quantidade do pedido for inválida (0 ou negativa)
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenQuantityIsZeroOrNegative()
        {
            // Arrange: criando dados de entrada para o comando com quantidade inválida
            var orderDto = new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 0 // Quantidade inválida para criar o pedido
            };
            var command = new CreateOrderCommand(orderDto);

            // Act: executando o comando com a quantidade inválida
            var result = await _handler.Handle(command, default);

            // Assert: verificando que o resultado foi uma falha
            Assert.False(result.IsSuccess); // O resultado deve ser uma falha
            Assert.Equal("Quantidade deve ser maior que zero.", result.ErrorMessage); // Mensagem de erro esperada
                                                                                      // Verifica que o método AddAsync **não** foi chamado, pois a validação falhou
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
        }

        // Teste para verificar se o comando retorna falha quando ocorre uma exceção durante a criação do pedido
        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenExceptionOccurs()
        {
            // Arrange: criando dados de entrada para o comando
            var orderDto = new CreateOrderDto
            {
                ProductId = 1,
                Quantity = 10
            };
            var command = new CreateOrderCommand(orderDto);

            // Arrange: configurando o mock do repositório para lançar uma exceção ao tentar adicionar um pedido
            _mockOrderRepository.Setup(repo => repo.AddAsync(It.IsAny<Order>()))
                .ThrowsAsync(new System.Exception("Erro ao adicionar o pedido"));

            // Act: executando o comando, que chamará o método Handle do handler
            var result = await _handler.Handle(command, default);

            // Assert: verificando que o resultado foi uma falha devido à exceção
            Assert.False(result.IsSuccess); // O resultado deve ser uma falha
            Assert.Equal("Erro ao criar o pedido: Erro ao adicionar o pedido", result.ErrorMessage); // Mensagem de erro esperada
                                                                                                     // Verifica se o método AddAsync foi chamado exatamente uma vez, mas com falha
            _mockOrderRepository.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
