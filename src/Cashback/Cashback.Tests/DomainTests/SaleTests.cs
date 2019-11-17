using Cashback.Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class SaleTests : IDisposable
	{
        List<Genre> genres;
        public SaleTests()
        {
            genres = new List<Genre>();

            var pop = new Genre(null, "Pop");
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Sunday, 25));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Monday, 7));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Tuesday, 6));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Wednesday, 2));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Thursday, 10));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Friday, 15));
            pop.AddCashbackConfig(new Domain.Models.Cashback(null, pop, DayOfWeek.Saturday, 20));


            var mpb = new Genre(null, "MPB");
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Sunday, 30));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Monday, 5));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Tuesday, 10));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Wednesday, 15));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Thursday, 20));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Friday, 25));
            mpb.AddCashbackConfig(new Domain.Models.Cashback(null, mpb, DayOfWeek.Saturday, 30));

            var classic = new Genre(null, "Classic");
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Sunday, 35));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Monday, 3));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Tuesday, 5));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Wednesday, 8));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Thursday, 13));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Friday, 18));
            classic.AddCashbackConfig(new Domain.Models.Cashback(null, classic, DayOfWeek.Saturday, 25));

            var rock = new Genre(null, "Rock");
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Sunday, 40));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Monday, 10));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Tuesday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Wednesday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Thursday, 15));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Friday, 20));
            rock.AddCashbackConfig(new Domain.Models.Cashback(null, rock, DayOfWeek.Saturday, 40));

            genres.Add(pop);
            genres.Add(mpb);
            genres.Add(classic);
            genres.Add(rock);
        }

        [Fact]
		public void SaleTest()
		{
            var album1 = new Album(null, null, "Test Album 1", genres[0]);
            var album2 = new Album(null, null, "Test Album 2", genres[1]);
            var album3 = new Album(null, null, "Test Album 2", genres[2]);
            var album4 = new Album(null, null, "Test Album 2", genres[3]);
            var sale = new Sale(null, "Test Customer");
            var saleItem1 = new SaleItem(sale, album1);
            var saleItem2 = new SaleItem(sale, album2);
            var saleItem3 = new SaleItem(sale, album3);
            var saleItem4 = new SaleItem(sale, album4);
            sale.AddItem(saleItem1);
            sale.AddItem(saleItem2);
            sale.AddItem(saleItem3);
            sale.AddItem(saleItem4);

            Assert.NotNull(sale);
            Assert.Equal((album1.Value + album2.Value + album3.Value + album4.Value), sale.TotalValue);
            Assert.Equal((saleItem1.CashbackValue + saleItem2.CashbackValue + saleItem3.CashbackValue + saleItem4.CashbackValue), sale.TotalCashback);

        }

        public void Dispose()
        {
        }
    }
}
