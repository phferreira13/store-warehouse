namespace warehouse.service.domain.Models
{
    public class Warehouse(string name, string location)
    {
        public int Id { get; private set; }
        public string Name { get; private set; } = name;
        public string Location { get; private set; } = location;
        public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; private set; }
        public List<WarehouseItem> Items { get; private set; } = [];

        public class WarehouseItem(int itemId, int quantity)
        {
            public int Id { get; private set; }
            public int ItemId { get; private set; } = itemId;
            public int Quantity { get; private set; } = quantity;
            public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
            public DateTime? UpdatedAt { get; private set; }
            public virtual Item Item { get; private set; }

            public void Update(int quantity)
            {
                Quantity = quantity;
                UpdatedAt = DateTime.UtcNow;
            }

            public WarehouseItem(Item item, int quantity) : this(item.Id, quantity)
            {
                Item = item;
            }
        }

        public void Update(string name, string location)
        {
            Name = name;
            Location = location;
            UpdatedAt = DateTime.UtcNow;
        }

        public void AddItem(int item, int quantity)
        {
            Items.Add(new WarehouseItem(item, quantity));
        }

        public void IncreaseItemQuantity(int itemId, int quantity = 1)
        {
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity must be greater than 0");
            }

            var item = GetWarehouseItem(itemId);
            item?.Update(item.Quantity + quantity);
        }

        public void DecreaseItemQuantity(int itemId, int quantity = 1)
        {
            if (quantity < 1)
            {
                throw new ArgumentException("Quantity must be greater than 0");
            }

            var item = GetWarehouseItem(itemId);
            item?.Update(item.Quantity - quantity);
        }

        public WarehouseItem? GetWarehouseItem(int itemId)
        {
            return Items.FirstOrDefault(i => i.ItemId == itemId);
        }
    }
}
