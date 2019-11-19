using Cashback.Domain.Events;
using Cashback.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Domain.Commands.Spotify
{
    public class SpotifyCommand : ISpotifyCommand, ICommand
    {

        public string RefreshToken { get; set; }
        public string AccessToken { get; set; }

        public async Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            return await Task.FromResult(new CommandResult(0));
        }

        public void Authorize()
        {

        }

        public void RefreshTokenAsync()
        {
            throw new NotImplementedException();
        }
        
        public EventType GetEvent()
        {
            return EventType.None;
        }

    }
}
