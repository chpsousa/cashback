using Cashback.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class SaleItemTests : IDisposable
	{
        List<Genre> genres;
        public SaleItemTests()
        {
            genres = new List<Genre>();

            var pop = new Genre(null, "Pop");
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Sunday, 25));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Monday, 7));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Tuesday, 6));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Wednesday, 2));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Thursday, 10));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Friday, 15));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop.Id, DayOfWeek.Saturday, 20));


            var mpb = new Genre(null, "MPB");
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Sunday, 30));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Monday, 5));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Tuesday, 10));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Wednesday, 15));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Thursday, 20));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Friday, 25));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb.Id, DayOfWeek.Saturday, 30));

            var classic = new Genre(null, "Classic");
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Sunday, 35));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Monday, 3));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Tuesday, 5));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Wednesday, 8));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Thursday, 13));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Friday, 18));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic.Id, DayOfWeek.Saturday, 25));

            var rock = new Genre(null, "Rock");
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Sunday, 40));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Monday, 10));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Tuesday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Wednesday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Thursday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Friday, 20));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock.Id, DayOfWeek.Saturday, 40));

            genres.Add(pop);
            genres.Add(mpb);
            genres.Add(classic);
            genres.Add(rock);
        }

        [Fact]
		public void CashbackValueTest()
		{
            var album1 = new Album(null, null, "Test Album 1", genres[0].Id);

            var sale = new Sale(null, "Test Customer");

            var obj = new SaleItem(sale.Id, album1.Id);

            Assert.NotNull(obj);
            Assert.Equal(obj.CashbackValue, (obj.Album.Value * (obj.Album.Genre.GetCashback(sale.Date.DayOfWeek) / 100)));
		}

        public void Dispose()
        {
        }
    }
}
