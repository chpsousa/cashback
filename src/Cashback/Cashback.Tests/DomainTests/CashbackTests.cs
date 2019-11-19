using Cashback.Domain.Models;
using System;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class CashbackTests : IDisposable
    {
        Genre genre;

        public CashbackTests()
        {
            genre = new Genre(null, "Test genre 1");
        }

        [Fact]
        public void CashbackTest()
        {
            var obj = new Cashback.Domain.Models.Cashback(null, genre.Id, DayOfWeek.Monday, 15);

            Assert.NotNull(obj);
            Assert.Equal(15, obj.Percent);
            Assert.Equal(DayOfWeek.Monday, obj.DayOfWeek);
            Assert.Equal(obj.GenreId, obj.Genre.Id);
        }

        public void Dispose()
        {
        }
    }
}
