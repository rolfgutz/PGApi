using MediatR;
using Microsoft.AspNetCore.Mvc;
using PGApi.Application.Commands;
using PGApi.Application.DTOs;
using PGApi.Application.Queries;

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
            var command = new CreateOrderCommand(orderDto);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok();
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
