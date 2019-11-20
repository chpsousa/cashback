using Cashback.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class SaleItemTests : IDisposable
	{
        List<Genre> genres;
        public SaleItemTests()
        {
            genres = new List<Genre>();
            var defaultCashback = new DefaultCashback();

            var pop = new Genre(null, "Pop");
            pop.AddCashbackConfigs(defaultCashback.GetDefaultCashback(pop.Id, pop.Name));

            var mpb = new Genre(null, "MPB");
            mpb.AddCashbackConfigs(defaultCashback.GetDefaultCashback(mpb.Id, mpb.Name));

            var classic = new Genre(null, "Classical");
            classic.AddCashbackConfigs(defaultCashback.GetDefaultCashback(classic.Id, classic.Name));

            var rock = new Genre(null, "Rock");
            rock.AddCashbackConfigs(defaultCashback.GetDefaultCashback(rock.Id, rock.Name));

            genres.Add(pop);
            genres.Add(mpb);
            genres.Add(classic);
            genres.Add(rock);
        }

        [Fact]
		public void CashbackValueTest()
		{
            var album1 = new Album(null, null, "Test Album 1", genres[0].Id);
            album1.Genre = genres[0];

            var sale = new Sale(null, "Test Customer");

            var obj = new SaleItem(sale.Id, album1.Id);
            obj.Album = album1;

            foreach (var genre in genres)
                Assert.Equal(7, genre.Cashbacks.Count());
            obj.CalcCashback();

            Assert.NotNull(obj);
            Assert.Equal(obj.CashbackValue, (obj.Album.Value * (obj.Album.Genre.GetCashback(sale.Date.DayOfWeek) / 100)));
		}

        public void Dispose()
        {
        }
    }
}
