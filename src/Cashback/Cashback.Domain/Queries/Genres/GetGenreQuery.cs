using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Genres
{
    public class GetGenreQuery : IQuery<GenreViewModel>
    {
        public string Id { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Id);
        }

        public async Task<GenreViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            var obj = await queriesHandler.DbContext
                .Genres
                .Include(i => i.Cashbacks)
                .Where(w => w.Id == Id)
                .Select(s =>
                    new GenreViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Cashbacks = s.Cashbacks.Select(c => new CashbackViewModel { DayOfWeek = c.DayOfWeek.ToString(), Percent = c.Percent }).ToList()
                    }).SingleOrDefaultAsync();

            if (obj == null)
                return new GenreViewModel[0];

            return new GenreViewModel[] { obj };
        }

    }
}
