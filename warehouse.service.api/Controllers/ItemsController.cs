using MediatR;
using Microsoft.AspNetCore.Mvc;
using warehouse.service.business.UseCases.Items;
using warehouse.service.domain.Models;

namespace warehouse.service.api.Controllers
{
    [ApiController]
    [Route("api/items")]
    public class ItemsController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Item>), 200)]
        public async Task<IActionResult> GetItems()
        {
            var items = await _mediator.Send(new GetItemsQuery());
            return Ok(items);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Item), 200)]
        public async Task<IActionResult> GetItemById(int id)
        {
            var item = await _mediator.Send(new GetItemByIdQuery { Id = id });
            return Ok(item);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Item), 201)]
        public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command)
        {
            var item = await _mediator.Send(command);
            return Ok(item);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateItem(int id, [FromBody] UpdateItemCommand command)
        {
            await _mediator.Send(command.SetId(id));
            return NoContent();
        }


    }
}
