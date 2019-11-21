using Cashback.Domain.Queries;
using System.Threading.Tasks;

namespace Cashback.Domain.Util
{
    public interface IQuery<T>
    {
        bool IsValid();
        Task<T[]> ExecuteAsync(CashbackQueriesHandler queriesHandler);
    }
}
