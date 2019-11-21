using Cashback.Domain.Commands;
using Cashback.Domain.Util;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Cashback.Domain.Queries
{
    public class CashbackQueriesHandler
    {
        public CashbackDbContext DbContext { get; private set; }

        public CashbackQueriesHandler(CashbackDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<T[]> RunQuery<T>(IQuery<T> query)
        {
            if (!query.IsValid())
                return null;
            return await query.ExecuteAsync(this);
        }
    }
}
