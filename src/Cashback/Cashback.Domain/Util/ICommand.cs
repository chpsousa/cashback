using Cashback.Domain.Commands;
using Cashback.Domain.Events;
using System.Threading.Tasks;

namespace Cashback.Domain.Util
{
    public interface ICommand
    {
        Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler);
        Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler);
        EventType GetEvent();
    }
}
