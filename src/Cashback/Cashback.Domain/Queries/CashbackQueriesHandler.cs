using Cashback.Domain.Commands;

namespace Cashback.Domain.Queries
{
    public class CashbackQueriesHandler
    {
        public CashbackDbContext DbContext { get; private set; }

        public CashbackQueriesHandler(CashbackDbContext dbContext)
        {
            DbContext = dbContext;
        }

        //public async Task<QueryResult<T>> RunQuery<T>(IQuery<T> query, bool calcTime = false) where T : class, IViewModel
        //{
        //    var sw = calcTime ? Stopwatch.StartNew() : null;
        //    var result = await _RunQuery(query);
        //    return _GetResult(result, sw);
        //}
    }
}
