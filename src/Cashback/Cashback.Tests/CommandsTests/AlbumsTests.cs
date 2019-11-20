using Cashback.Domain.Commands;
using Cashback.Domain.Commands.Albums;
using Cashback.Domain.Commands.Genres;
using Cashback.Domain.Commands.Spotify;
using Cashback.Domain.Models;
using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Cashback.Tests.CommandsTests
{
    [Collection("Empty database collection")]
    public class AlbumsTests
    {
        public Empty_DbContextFixture Fixture { get; private set; }
        public CashbackDbContext DbContext { get; private set; }
        public CashbackCommandsHandler CommandsHandler { get; private set; }

        public AlbumsTests(Empty_DbContextFixture fixture)
        {
            Fixture = fixture;
            DbContext = fixture.DbContext;
            CommandsHandler = fixture.CommandsHandler;
        }

        [Fact]
        public async void CreateAlbumTest()
        {
            var genreId = RandomId.NewId();
            var objGenre = new Genre(genreId, "genre test");
            objGenre.AddCashbackConfig(new Domain.Models.Cashback(null, genreId, System.DayOfWeek.Monday, 10));
            await DbContext.Genres.AddAsync(objGenre);
            await DbContext.SaveChangesAsync();

            var id = RandomId.NewId();
            var cmd = new CreateAlbumCommand()
            {
                Id = id,
                Name = "Test Genre 1",
                GenreId = genreId,
                SpotifyId = null
            };

            var result = await CommandsHandler.Handle(cmd);

            var obj = await DbContext.Albums.Where(w => w.Id == id).FirstOrDefaultAsync();

            Assert.NotNull(result);
            Assert.NotNull(obj);
            Assert.Equal(ErrorCode.None, result.ErrorCode);
            Assert.Equal(id, obj.Id);
            Assert.True(result.Rows > 0);
        }

        [Fact]
        public async void PopulateAlbumsTest()
        {
            var cmd = new PopulateAlbumCommand();
            var result = await CommandsHandler.Handle(cmd);
            var albums = DbContext.Albums.ToList();

            Assert.Equal(ErrorCode.None, result.ErrorCode);
            Assert.NotNull(albums);
            Assert.Equal(200, albums.Count());
            Assert.Equal(50, albums.Where(w => w.Genre.Name == "Pop").Count());
            Assert.Equal(50, albums.Where(w => w.Genre.Name == "Rock").Count());
            Assert.Equal(50, albums.Where(w => w.Genre.Name == "MPB").Count());
            Assert.Equal(50, albums.Where(w => w.Genre.Name == "Classical").Count());
        }
    }
}
