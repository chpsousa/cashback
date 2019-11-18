using System.Linq;
using System.Threading.Tasks;
using Cashback.Domain.Events;
using Cashback.Domain.Models;
using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Domain.Commands.Albums
{
    public class CreateAlbumCommand : ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string SpotifyId { get; set; }
        public string GenreId { get; set; }

        public async Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            var obj = await handler.DbContext.Albums.Where(w => w.Id == Id).FirstOrDefaultAsync();
            if (obj != null)
                return await Task.FromResult(new CommandResult(0, ErrorCode.DuplicateUniqueIdentifier));

            var objGenre = await handler.DbContext.Genres.Where(w => w.Id == GenreId).FirstOrDefaultAsync();
            if (objGenre == null)
                return await Task.FromResult(new CommandResult(0, ErrorCode.NotFound, "Genre with id was not found"));

            obj = new Album(Id, SpotifyId, Name, objGenre);

            await handler.DbContext.Albums.AddAsync(obj);
            var rows = await handler.DbContext.SaveChangesAsync();

            return await Task.FromResult(new CommandResult(rows, ErrorCode.None));
        }

        public EventType GetEvent()
        {
            throw new System.NotImplementedException();
        }
    }
}
