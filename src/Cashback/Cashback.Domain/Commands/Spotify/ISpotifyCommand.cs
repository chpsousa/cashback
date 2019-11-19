using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Commands.Spotify
{
    public interface ISpotifyCommand 
    {
        void RefreshTokenAsync();
        void Authorize();

    }
}
