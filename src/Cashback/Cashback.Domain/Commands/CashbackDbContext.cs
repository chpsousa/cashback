using Cashback.Domain.Models;
using Cashback.Domain.Models.DbConfig;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cashback.Domain.Commands
{
    public class CashbackDbContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }
        public DbSet<Cashback.Domain.Models.Cashback> Cashbacks { get; set; }

        public CashbackDbContext(DbContextOptions<CashbackDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("cashback");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AlbumDbConfig());
            builder.ApplyConfiguration(new GenreDbConfig());
            builder.ApplyConfiguration(new CashbackDbConfig());
            builder.ApplyConfiguration(new SaleDbConfig());
            builder.ApplyConfiguration(new SaleItemDbConfig());
        }
    }
}
