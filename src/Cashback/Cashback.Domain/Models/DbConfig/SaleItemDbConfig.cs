using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cashback.Domain.Models.DbConfig
{
    public class SaleItemDbConfig : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("genres", schema: "dbo");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Id).HasMaxLength(8).HasColumnType("char(8)");
            builder.Property(p => p.CashbackValue).HasColumnName("cashback_value");
            builder.Property(p => p.AlbumId).HasColumnName("id_album").IsRequired();
            builder.HasOne(p => p.Album);
            builder.Property(p => p.SaleId).HasColumnName("id_sale").IsRequired();
            builder.HasOne(p => p.Sale);
            builder.HasQueryFilter(p => !p.Removed);
        }
    }
}
