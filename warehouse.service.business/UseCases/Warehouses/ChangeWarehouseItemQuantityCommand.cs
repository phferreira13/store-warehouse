namespace warehouse.service.business.UseCases.Warehouses
{
    public class ChangeWarehouseItemQuantityCommand : IRequest<Warehouse>
    {
        private int _warehouseId;
        public void SetWarehouseId(int warehouseId) => _warehouseId = warehouseId;
        public int ItemId { get; set; }
        public int Quantity { get; set; }

        internal class Handler(IWarehouseRepository warehouseRepository, IItemRepository itemRepository) : IRequestHandler<ChangeWarehouseItemQuantityCommand, Warehouse>
        {

            public async Task<Warehouse> Handle(ChangeWarehouseItemQuantityCommand request, CancellationToken cancellationToken)
            {
                var warehouse = await warehouseRepository.GetWarehouseAsync(request._warehouseId);
                if (warehouse == null)
                {
                    throw new ArgumentException($"Warehouse with id {request._warehouseId} not found");
                }

                var item = warehouse.GetWarehouseItem(request.ItemId);
                if (item == null)
                {
                    if (request.Quantity < 0)
                    {
                        throw new ArgumentException($"Item with id {request.ItemId} not found in warehouse with id {request._warehouseId}");
                    }
                    else
                    {
                        var itemEntity = await itemRepository.GetItemAsync(request.ItemId)
                            ?? throw new ArgumentException($"Item with id {request.ItemId} not found");
                        await warehouseRepository.AddItemAsync(request._warehouseId, itemEntity, request.Quantity);
                        return warehouse;
                    }
                }

                if (request.Quantity < 0)
                {
                    await warehouseRepository.DecreaseItemQuantityAsync(request._warehouseId, request.ItemId, Math.Abs(request.Quantity));
                }
                else
                {
                    await warehouseRepository.IncreaseItemQuantityAsync(request._warehouseId, request.ItemId, request.Quantity);
                }
                

                return warehouse;
            }
        }
    }
}
