using Cashback.Domain.Commands;
using Cashback.Domain.Commands.Genres;
using Cashback.Domain.Models;
using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cashback.Tests.CommandsTests
{
    [Collection("Empty database collection")]
    public class GenresTests
    {
        public Empty_DbContextFixture Fixture { get; private set; }
        public CashbackDbContext DbContext { get; private set; }

        public CashbackCommandsHandler CommandsHandler { get; private set; }


        public GenresTests(Empty_DbContextFixture fixture)
        {
            Fixture = fixture;
            DbContext = fixture.DbContext;
            CommandsHandler = fixture.CommandsHandler;
        }

        [Fact]
        public async void CreateGenreTest()
        {
            var id = RandomId.NewId();
            var cashback = new List<CashbackCommand>();
            cashback.Add(new CashbackCommand() { Id = null, Percent = 15, DayOfWeek = System.DayOfWeek.Monday, GenreId = id });
            cashback.Add(new CashbackCommand() { Id = null, Percent = 10, DayOfWeek = System.DayOfWeek.Saturday, GenreId = id });
            cashback.Add(new CashbackCommand() { Id = null, Percent = 5, DayOfWeek = System.DayOfWeek.Sunday, GenreId = id });

            var cmd = new CreateGenreCommand()
            {
                Id = id,
                Name = "Test Genre 1",
                Cashback = cashback
            };

            var result = await CommandsHandler.Handle(cmd);

            var obj = await DbContext.Genres.Where(w => w.Id == id).FirstOrDefaultAsync();

            Assert.NotNull(result);
            Assert.NotNull(obj);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
            Assert.Equal(id, obj.Id);
            Assert.True(result.Rows > 0);
        }
    }
}
