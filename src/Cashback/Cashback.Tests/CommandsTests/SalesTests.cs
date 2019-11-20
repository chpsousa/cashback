using Cashback.Domain.Commands;
using Cashback.Domain.Commands.Sales;
using Cashback.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cashback.Tests.CommandsTests
{
    [Collection("Empty database collection")]
    public class SalesTests
    {
        public Empty_DbContextFixture Fixture { get; private set; }
        public CashbackDbContext DbContext { get; private set; }

        public CashbackCommandsHandler CommandsHandler { get; private set; }


        public SalesTests(Empty_DbContextFixture fixture)
        {
            Fixture = fixture;
            DbContext = fixture.DbContext;
            CommandsHandler = fixture.CommandsHandler;
        }

        [Fact]
        public async void CreateSaleTest()
        {
            var genre = new Genre(null, "Pop");
            new DefaultCashback().GetDefaultCashback(genre.Id, genre.Name);
            var album = new Album(null, "spotifyrandomid", "new album", genre.Id);
            var album2 = new Album(null, "spotifyrandomid2", "new album 2", genre.Id);

            await CommandsHandler.DbContext.Genres.AddAsync(genre);
            await CommandsHandler.DbContext.Cashbacks.AddRangeAsync(genre.Cashbacks);
            await CommandsHandler.DbContext.Albums.AddAsync(album);
            await CommandsHandler.DbContext.Albums.AddAsync(album2);
            await CommandsHandler.DbContext.SaveChangesAsync();

            var items = new List<SaleItemCommand>();
            items.Add(new SaleItemCommand() { AlbumId = album.Id });
            items.Add(new SaleItemCommand() { AlbumId = album2.Id });

            var cmd = new CreateSaleCommand()
            {
                CustomerName = "new customer name",
                Items = items.ToArray()
            };

            var result = await CommandsHandler.Handle(cmd);

            var obj = await DbContext.Sales.SingleOrDefaultAsync();

            Assert.True(result.Rows > 0);
            Assert.NotNull(obj);
            Assert.Equal(items.Count(), obj.Items.Count());
            Assert.Equal(obj.Items.Sum(s => s.Album.Value), obj.TotalValue);
            Assert.Equal(obj.Items.Sum(s => s.CashbackValue), obj.TotalCashback);
        }
    }
}
