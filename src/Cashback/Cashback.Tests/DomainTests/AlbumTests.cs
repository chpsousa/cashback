using Cashback.Domain.Models;
using System;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class AlbumTests : IDisposable
    {
        Genre genre;

        public AlbumTests()
        {
            genre = new Genre(null, "Test genre 1");
        }

        [Fact]
        public void AlbumTest()
        {
            var obj = new Album(null, null, "Test album 1", genre.Id);

            Assert.NotNull(obj);
            Assert.Equal(8, obj.Id.Length);
            Assert.NotNull(obj.Name);
            Assert.True(obj.Value >= 1 && obj.Value <= 50);
        }

        public void Dispose()
        {
        }
    }
}
