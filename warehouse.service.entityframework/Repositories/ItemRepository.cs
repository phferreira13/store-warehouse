using Microsoft.EntityFrameworkCore;
using warehouse.service.domain.Interfaces.Repositories;
using warehouse.service.domain.Models;
using warehouse.service.entityframework.Context;

namespace warehouse.service.entityframework.Repositories;

public class ItemRepository : IItemRepository
{
    private readonly WarehouseContext _context;

    public ItemRepository(WarehouseContext context)
    {
        _context = context;
    }

    public async Task<Item?> GetItemAsync(int id)
    {
        return await _context.Items.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Item>> GetItemsAsync()
    {
        return await _context.Items.ToListAsync();
    }

    public async Task AddItemAsync(Item item)
    {
        _context.Items.Add(item);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateItemAsync(int id, string name, decimal price, string description)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            item.Update(name, price, description);
            try
            {
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


        }
    }

    public async Task DeleteItemAsync(int id)
    {
        var item = await _context.Items.FindAsync(id);
        if (item != null)
        {
            _context.Items.Remove(item);
            await _context.SaveChangesAsync();
        }
    }


    public async Task UpdateItemAsync(Item item)
    {
        _context.Items.Update(item);
        await _context.SaveChangesAsync();
    }
}
