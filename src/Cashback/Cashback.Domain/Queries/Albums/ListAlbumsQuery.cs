using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Albums
{
    public class ListAlbumsQuery : IQuery<AlbumViewModel>
    {
        public async Task<AlbumViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            return queriesHandler.DbContext
                .Albums
                .Include(i => i.Genre)
                .Select(s =>
                    new AlbumViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        SpotifyId = s.SpotifyId,
                        GenreName = s.Genre.Name,
                        GenreId = s.Genre.Id,
                        Value = s.Value
                    }).ToArray();
        }

        public bool IsValid()
        {
            return true;
        }
    }

    public class AlbumViewModel : IViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SpotifyId { get; set; }
        public string GenreName { get; set; }
        public string GenreId { get; set; }
        public decimal Value { get; set; }
    }
}
