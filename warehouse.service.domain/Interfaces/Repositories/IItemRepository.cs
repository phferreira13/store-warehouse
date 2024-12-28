using warehouse.service.domain.Models;

namespace warehouse.service.domain.Interfaces.Repositories
{
    public interface IItemRepository
    {
        Task<Item?> GetItemAsync(int id);
        Task<IEnumerable<Item>> GetItemsAsync();
        Task AddItemAsync(Item item);
        Task UpdateItemAsync(int id, string name, decimal price, string description);
        Task DeleteItemAsync(int id);
        Task UpdateItemAsync(Item item);
    }
}
