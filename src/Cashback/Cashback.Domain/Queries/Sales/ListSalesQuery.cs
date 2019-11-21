using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Sales
{
    public class ListSalesQuery : IQuery<SaleViewModel>
    {
        public async Task<SaleViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            return queriesHandler.DbContext
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
                            new SaleItemViewModel {
                                Id = c.Id,
                                SaleId = c.SaleId,
                                AlbumId = c.AlbumId,
                                AlbumName = c.Album.Name,
                                AlbumValue = c.Album.Value,
                                GenreName = c.Album.Genre.Name,
                                Cashback = c.CashbackValue
                            }).ToList()
                    }).ToArray();
        }

        public bool IsValid()
        {
            return true;
        }
    }

    public class SaleViewModel : IViewModel
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalCashback { get; set; }
        public List<SaleItemViewModel> Items { get; set; }
    }

    public class SaleItemViewModel : IViewModel
    {
        public string Id { get; set; }
        public string SaleId { get; set; }
        public string AlbumId { get; set; }
        public string AlbumName { get; set; }
        public decimal AlbumValue { get; set; }
        public string GenreName { get; set; }
        public decimal Cashback { get; set; }
    }
}
