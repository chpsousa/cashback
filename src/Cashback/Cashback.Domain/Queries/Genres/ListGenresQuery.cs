using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries.Genres
{
    public class ListGenresQuery : IQuery<GenreViewModel>
    {
        public async Task<GenreViewModel[]> ExecuteAsync(CashbackQueriesHandler queriesHandler)
        {
            return queriesHandler.DbContext
                .Genres
                .Include(i => i.Cashbacks)
                .Select(s =>
                    new GenreViewModel()
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Cashbacks = s.Cashbacks.Select(c => new CashbackViewModel { DayOfWeek = c.DayOfWeek.ToString(), Percent = c.Percent }).ToList()
                    }).ToArray();
        }

        public bool IsValid()
        {
            return true;
        }
    }

    public class GenreViewModel : IViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CashbackViewModel> Cashbacks { get; set; }
    }

    public class CashbackViewModel : IViewModel
    {
        public string DayOfWeek { get; set; }
        public decimal Percent { get; set; }
    }
}
