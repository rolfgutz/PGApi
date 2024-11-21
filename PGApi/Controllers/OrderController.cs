using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGApi.Application.Commands;
using PGApi.Application.Commands.CreateOrder;
using PGApi.Application.Commands.DeleteOrder;
using PGApi.Application.Commands.UpdateOrder;
using PGApi.Application.DTOs;
using PGApi.Application.Queries;
using PGApi.Application.Queries.GetAllOrders;

namespace PGApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto orderDto)
        {
            // Cria um comando para passar os dados.
            var command = new CreateOrderCommand(orderDto);
            // Envia o comando usando MediatR para o handler correspondente.
            var result = await _mediator.Send(command); 

            if (result.IsSuccess)
            {
                return Ok(); // Se o comando for bem-sucedido, retorna status 200 OK.
            }

            return BadRequest(result.ErrorMessage); // Se falhar, retorna status 400 BadRequest com a mensagem de erro.
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery(); // Cria a consulta
            var result = await _mediator.Send(query); // Envia para o MediatR e executa o handler

            if (result.IsSuccess)
            {
                return Ok(result); // Retorna os dados com status 200 OK
            }

            return BadRequest(result.ErrorMessage); // Caso haja erro, retorna 400 com a mensagem
        }

        /// <summary>
        /// Atualiza um pedido existente.
        /// </summary>
        /// <param name="Id">ID do pedido a ser atualizado.</param>
        /// <param name="orderDto">Dados para atualização do pedido.</param>
        /// <returns>Resultado da operação.</returns>
        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateOrder(int Id, [FromBody] UpdateOrderDto orderDto)
        {
            if (orderDto == null)
                return BadRequest("Dados do pedido inválidos.");


            // Garante que o ID no comando corresponda ao ID da rota
            var command = new UpdateOrderCommand(Id, orderDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok("Pedido atualizado com sucesso!");

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteOrder(int Id)
        {
            var command = new DeleteOrderCommand(Id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok("Pedido deletado com sucesso.");

            return BadRequest(result.ErrorMessage);
        }
    }
}
