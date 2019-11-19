using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cashback.Domain.Events;
using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Domain.Commands.Genres
{
    public class CreateGenreCommand : ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<CashbackCommand> Cashback { get; set; }

        public Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            return Task.FromResult(new CommandResult(ErrorCode.None));
        }
        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            var obj = await handler.DbContext.Genres.Where(w => w.Id == Id).FirstOrDefaultAsync();
            if (obj != null)
                return await Task.FromResult(new CommandResult(0, ErrorCode.DuplicateUniqueIdentifier));

            obj = new Models.Genre(Id, Name);
            foreach (var item in Cashback)
                obj.AddCashbackConfig(new Models.Cashback(item.Id, obj.Id, item.DayOfWeek, item.Percent));

            await handler.DbContext.Genres.AddAsync(obj);
            await handler.DbContext.Cashbacks.AddRangeAsync(obj.Cashbacks);
            var rows = await handler.DbContext.SaveChangesAsync();

            return await Task.FromResult(new CommandResult(rows, ErrorCode.None));
        }
        public EventType GetEvent()
        {
            return EventType.None;
        }
    }
}
