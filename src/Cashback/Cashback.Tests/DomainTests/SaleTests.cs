using Cashback.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public void SaleTest()
		{
            var album1 = new Album(null, null, "Test Album 1", genres[0].Id);
            album1.Genre = genres[0];
            var album2 = new Album(null, null, "Test Album 2", genres[1].Id);
            album2.Genre = genres[1];
            var album3 = new Album(null, null, "Test Album 2", genres[2].Id);
            album3.Genre = genres[2];
            var album4 = new Album(null, null, "Test Album 2", genres[3].Id);
            album4.Genre = genres[3];
            var sale = new Sale(null, "Test Customer");
            var saleItem1 = new SaleItem(sale.Id, album1.Id);
            saleItem1.Album = album1;
            saleItem1.Sale = sale;
            var saleItem2 = new SaleItem(sale.Id, album2.Id);
            saleItem2.Album = album2;
            saleItem2.Sale = sale;
            var saleItem3 = new SaleItem(sale.Id, album3.Id);
            saleItem3.Album = album3;
            saleItem3.Sale = sale;
            var saleItem4 = new SaleItem(sale.Id, album4.Id);
            saleItem4.Album = album4;
            saleItem4.Sale = sale;

            saleItem1.CalcCashback();
            saleItem2.CalcCashback();
            saleItem3.CalcCashback();
            saleItem4.CalcCashback();

            sale.AddItem(saleItem1);
            sale.AddItem(saleItem2);
            sale.AddItem(saleItem3);
            sale.AddItem(saleItem4);

            sale.TotalCashback = sale.Items.Sum(s => s.CashbackValue);
            sale.TotalValue = sale.Items.Sum(s => s.Album.Value);

            Assert.NotNull(sale);
            Assert.Equal((album1.Value + album2.Value + album3.Value + album4.Value), sale.TotalValue);
            Assert.Equal((saleItem1.CashbackValue + saleItem2.CashbackValue + saleItem3.CashbackValue + saleItem4.CashbackValue), sale.TotalCashback);

        }

        public void Dispose()
        {
        }
    }
}
