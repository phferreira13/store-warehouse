namespace warehouse.service.business.UseCases.Warehouses;
public class UpdateWarehouseCommand : IRequest
{
    private int Id { get; set; }
    public required string Name { get; set; }
    public required string Location { get; set; }
    public UpdateWarehouseCommand SetId(int id)
    {
        Id = id;
        return this;
    }

    internal class Handler(IWarehouseRepository warehouseRepository) : IRequestHandler<UpdateWarehouseCommand>
    {
        public async Task Handle(UpdateWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await warehouseRepository.GetWarehouseAsync(request.Id)
                ?? throw new Exception("Warehouse not found");
            warehouse.Update(request.Name, request.Location);

            await warehouseRepository.UpdateWarehouseAsync(warehouse);
        }
    }
}