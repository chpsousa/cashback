using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cashback.Domain.Models.DbConfig
{
    public class CashbackDbConfig : IEntityTypeConfiguration<Cashback>
    {
        public void Configure(EntityTypeBuilder<Cashback> builder)
        {
            builder.ToTable("cashbacks", schema: "dbo");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Id).HasMaxLength(8).HasColumnType("char(8)");
            builder.Property(p => p.DayOfWeek).HasColumnName("day_of_week");
            builder.Property(p => p.Percent).HasColumnName("percent");
            builder.Property(p => p.GenreId).HasColumnName("id_genre").IsRequired();
            builder.HasOne(p => p.Genre);
            builder.HasQueryFilter(p => !p.Removed);
        }
    }
}
