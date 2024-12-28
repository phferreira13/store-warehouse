using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using warehouse.service.domain.Models;

namespace warehouse.service.entityframework.Configurations;
public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.ToTable("Warehouses");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Location).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);

        builder.OwnsMany(x => x.Items, warehouseItem =>
        {
            warehouseItem.HasKey(x => x.Id);
            warehouseItem.Property(x => x.Id).ValueGeneratedOnAdd();
            warehouseItem.Property(x => x.Quantity).IsRequired();
            warehouseItem.Property(x => x.CreatedAt).IsRequired();
            warehouseItem.Property(x => x.UpdatedAt);
            warehouseItem.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId);
        });
    }
}
