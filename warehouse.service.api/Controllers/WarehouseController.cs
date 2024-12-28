using MediatR;
using Microsoft.AspNetCore.Mvc;
using warehouse.service.business.UseCases.Warehouses;
using warehouse.service.domain.Models;

namespace warehouse.service.api.Controllers
{
    [ApiController]
    [Route("api/warehouses")]
    public class WarehouseController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Warehouse>), 200)]
        public async Task<IActionResult> GetWarehouses()
        {
            var warehouses = await mediator.Send(new GetWarehousesQuery());
            return Ok(warehouses);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Warehouse), 200)]
        public async Task<IActionResult> GetWarehouseById(int id)
        {
            var warehouse = await mediator.Send(new GetWarehouseById { Id = id });
            return Ok(warehouse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Warehouse), 200)]
        public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseCommand command)
        {
            var warehouse = await mediator.Send(command);
            return Ok(warehouse);
        }

        [HttpPost("{id}/items")]
        [ProducesResponseType(typeof(Warehouse), 200)]
        public async Task<IActionResult> ChangeWarehouseItemQuantity(int id, [FromBody] ChangeWarehouseItemQuantityCommand command)
        {
            command.SetWarehouseId(id);
            var warehouse = await mediator.Send(command);
            return Ok(warehouse);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> UpdateWarehouse(int id, [FromBody] UpdateWarehouseCommand command)
        {
            await mediator.Send(command.SetId(id));
            return NoContent();
        }
    }
}
