using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using warehouse.service.domain.Models;

namespace warehouse.service.entityframework.Configurations;
public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);

        builder.OwnsMany(x => x.History, history =>
        {
            history.HasKey(x => x.Id);
            history.Property(x => x.Id).ValueGeneratedOnAdd();
            history.Property(x => x.Name).IsRequired();
            history.Property(x => x.Price).IsRequired();
            history.Property(x => x.Description).IsRequired();
            history.Property(x => x.UpdatedAt).IsRequired();
        });
    }
}
