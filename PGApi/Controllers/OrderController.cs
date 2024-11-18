using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGApi.Application.Commands;
using PGApi.Application.Commands.CreateOrder;
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
    }
}
