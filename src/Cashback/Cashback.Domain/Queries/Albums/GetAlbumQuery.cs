using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Albums
{
    public class GetAlbumQuery : IQuery<AlbumViewModel>
    {
        public string Id { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        public async Task<AlbumViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            var obj = await queriesHandler.DbContext
                .Albums
                .Include(i => i.Genre)
                .Where(w => w.Id == Id)
                .Select(s =>
                    new AlbumViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        SpotifyId = s.SpotifyId,
                        GenreName = s.Genre.Name,
                        GenreId = s.Genre.Id,
                        Value = s.Value
                    }).SingleOrDefaultAsync();

            if (obj == null)
                return new AlbumViewModel[0];

            return new AlbumViewModel[] { obj };
        }

    }
}
