using Cashback.Domain.Events;
using Cashback.Domain.Util;
using System.Threading.Tasks;

namespace Cashback.Domain.Commands
{
    public class CashbackCommandsHandler
    {
        public CashbackDbContext DbContext { get; private set; }

        public CashbackCommandsHandler(CashbackDbContext context)
        {
            DbContext = context;
        }

        public async Task<CommandResult> Handle(ICommand command)
        {
            var result = await command.GetErrorAsync(this);
            if (result != null && result.ErrorCode != ErrorCode.None)
                return result;

            result = await command.ExecuteAsync(this);
            if (result != null && result.ErrorCode != ErrorCode.None)
                return result;
            await EventsHandler.GetInstance().HandleEvent(this.DbContext, command, result);
            return result;
        }
    }
}
