namespace warehouse.service.domain.Models;

public class Item(string name, decimal price, string description)
{
    public int Id { get; private set; }
    public string Name { get; private set; } = name;
    public decimal Price { get; private set; } = price;
    public string Description { get; private set; } = description;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; private set; }
    public List<ItemHistory> History { get; private set; } = [];

    public static implicit operator ItemHistory(Item item)
    {
        return new ItemHistory(item.Name, item.Price, item.Description);
    }

    public class ItemHistory(string name, decimal price, string description)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public decimal Price { get; private set; } = price;
        public string Description { get; private set; } = description;
        public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    }

    public void Update(string name, decimal price, string description)
    {
        History.Add(this);
        Name = name;
        Price = price;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
    }
}
