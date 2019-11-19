using Cashback.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cashback.Tests.DomainTests
{
    public class GenreTests : IDisposable
    {
        public GenreTests()
        {
        }

        [Fact]
        public void GenreTest()
        {
            var obj = new Genre(null, "test genre 1");

            Assert.NotNull(obj);
            Assert.Equal(8, obj.Id.Length);
            Assert.NotNull(obj.Name);
        }

        [Fact]
        public void GenreTest2()
        {
            var obj = new Genre(null);

            Assert.NotNull(obj);
            Assert.Equal(8, obj.Id.Length);
            Assert.Null(obj.Name);
        }

        [Fact]
        public void AddCashbackConfigTest()
        {
            var obj = new Genre(null, "test genre 2");
            obj.AddCashbackConfig(new Domain.Models.Cashback(null, obj.Id, DayOfWeek.Monday, 15));

            Assert.NotNull(obj);
            Assert.Equal(8, obj.Id.Length);
            Assert.NotNull(obj.Name);
            Assert.NotNull(obj.Cashbacks);
            Assert.Single(obj.Cashbacks);
        }

        [Fact]
        public void AddCashbackConfigsTest()
        {
            var obj = new Genre(null, "test genre 2");
            var cashbacks = new List<Cashback.Domain.Models.Cashback>();
            cashbacks.Add(new Domain.Models.Cashback(null, obj.Id, DayOfWeek.Monday, 15));
            cashbacks.Add(new Domain.Models.Cashback(null, obj.Id, DayOfWeek.Tuesday, 10));

            obj.AddCashbackConfigs(cashbacks);

            Assert.NotNull(obj);
            Assert.Equal(8, obj.Id.Length);
            Assert.NotNull(obj.Name);
            Assert.NotNull(obj.Cashbacks);
            Assert.Equal(2, obj.Cashbacks.Count());
        }

        public void Dispose()
        {
        }
    }
}
