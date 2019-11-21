using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Sales
{
    public class GetSaleQuery : IQuery<SaleViewModel>
    {
        public string Id { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        public async Task<SaleViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            var obj = await queriesHandler.DbContext
                .Sales
                .Include(i => i.Items)
                .Select(s =>
                    new SaleViewModel()
                    {
                        Id = s.Id,
                        Date = s.Date,
                        CustomerName = s.CustomerName,
                        TotalValue = s.TotalValue,
                        TotalCashback = s.TotalCashback,
                        Items = s.Items
                        .Select(c =>
                            new SaleItemViewModel
                            {
                                Id = c.Id,
                                SaleId = c.SaleId,
                                AlbumId = c.AlbumId,
                                AlbumName = c.Album.Name,
                                AlbumValue = c.Album.Value,
                                Cashback = c.CashbackValue
                            }).ToList()
                    }).SingleOrDefaultAsync();

            if (obj == null)
                return new SaleViewModel[0];

            return new SaleViewModel[] { obj };
        }

    }
}
