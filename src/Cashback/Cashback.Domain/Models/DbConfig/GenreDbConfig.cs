using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cashback.Domain.Models.DbConfig
{
    public class GenreDbConfig : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            var emptyAsNull = new ValueConverter<string, string>(value => value == string.Empty ? null : value, value => value);
            builder.ToTable("genres", schema: "dbo");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Id).HasMaxLength(8).HasColumnType("char(8)");
            builder.Property(p => p.Name).HasMaxLength(150).HasColumnName("name").HasConversion(emptyAsNull);
            builder.HasMany(p => p.Cashbacks).WithOne(p => p.Genre).HasForeignKey(p => p.GenreId);
            builder.HasQueryFilter(p => !p.Removed);
        }
    }
}
