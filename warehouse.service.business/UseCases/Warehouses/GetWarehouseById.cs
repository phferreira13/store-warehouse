namespace warehouse.service.business.UseCases.Warehouses;
public class GetWarehouseById : IRequest<Warehouse?>
{
    public int Id { get; set; }

    internal class Handler(IWarehouseRepository warehouseRepository) : IRequestHandler<GetWarehouseById, Warehouse?>
    {
        public async Task<Warehouse?> Handle(GetWarehouseById request, CancellationToken cancellationToken)
        {
            return await warehouseRepository.GetWarehouseAsync(request.Id);
        }
    }
}