namespace warehouse.service.business.UseCases.Warehouses
{
    public class GetWarehousesQuery : IRequest<IEnumerable<Warehouse>>
    {
        internal class Handler(IWarehouseRepository warehouseRepository) : IRequestHandler<GetWarehousesQuery, IEnumerable<Warehouse>>
        {
            public async Task<IEnumerable<Warehouse>> Handle(GetWarehousesQuery request, CancellationToken cancellationToken)
            {
                return await warehouseRepository.GetWarehousesAsync();
            }
        }
    }
}
