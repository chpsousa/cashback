using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cashback.Domain.Models.DbConfig
{
    public class SaleDbConfig : IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("sales", schema: "dbo");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Id).HasMaxLength(8).HasColumnType("char(8)");
            builder.Property(p => p.CustomerName).HasColumnName("customer_name");
            builder.Property(p => p.Date).HasColumnName("date");
            builder.Property(p => p.TotalValue).HasColumnName("total_value");
            builder.Property(p => p.TotalCashback).HasColumnName("total_cashback");
            builder.HasMany(p => p.Items).WithOne(p => p.Sale).HasForeignKey(p => p.SaleId);
            builder.HasQueryFilter(p => !p.Removed);
        }
    }
}
