using warehouse.service.domain.Models;

namespace warehouse.service.domain.Interfaces.Repositories
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetWarehouseAsync(int id);
        Task<IEnumerable<Warehouse>> GetWarehousesAsync();
        Task AddWarehouseAsync(Warehouse warehouse);
        Task UpdateWarehouseAsync(int id, string name, string location);
        Task AddItemAsync(int warehouseId, Item item, int quantity);
        Task IncreaseItemQuantityAsync(int warehouseId, int itemId, int quantity = 1);
        Task DecreaseItemQuantityAsync(int warehouseId, int itemId, int quantity = 1);
        Task DeleteWarehouseAsync(int id);
        Task UpdateWarehouseAsync(Warehouse warehouse);
    }
}
