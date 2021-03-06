using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace Cashback.Domain.Models.DbConfig
{
    public class AlbumDbConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            var emptyAsNull = new ValueConverter<string, string>(value => value == string.Empty ? null : value, value => value);
            builder.ToTable("albums", schema: "dbo");
            builder.HasKey(p => p.Id).HasName("id");
            builder.Property(p => p.Id).HasMaxLength(8).HasColumnType("char(8)");
            builder.Property(p => p.SpotifyId);
            builder.Property(p => p.Name).HasMaxLength(150).HasColumnName("name").HasConversion(emptyAsNull);
            builder.Property(p => p.GenreId).HasColumnName("id_genre").IsRequired();
            builder.HasOne(p => p.Genre);

            builder.HasQueryFilter(p => !p.Removed);
        }
    }
}
