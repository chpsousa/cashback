using Cashback.Domain.Commands.Albums;
using Cashback.Domain.Events;
using Cashback.Domain.Util;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cashback.Domain.Commands.Spotify
{
    public class PopulateAlbumCommand : SpotifyCommand, ICommand
    {
        public async Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            if (handler.DbContext.Albums.Count() > 0)
                return await Task.FromResult(new CommandResult(ErrorCode.None));

            var genres = handler.DbContext.Genres;
            if (genres == null || genres.Count() == 0)
                await handler.Handle(new PopulateGenresCommand());

            if (string.IsNullOrEmpty(AccessToken))
                Authorize();

            IList<AlbumViewModel> result = new List<AlbumViewModel>();

            HttpClient httpClient = new HttpClient();
            var rows = 0;

            HttpResponseMessage response = null;
            foreach (var genre in genres)
            {
                var baseUrl = "search?query={genre}&type=album&market=BR&offset=0&limit=50";
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
                response = await httpClient.GetAsync("https://api.spotify.com/v1/" + baseUrl.Replace("{genre}", genre.Name.ToLower()));

                if (response.IsSuccessStatusCode)
                {
                    var json = JsonConvert.DeserializeAnonymousType(await response.Content.ReadAsStringAsync(),
                        new
                        {
                            albums = new
                            {
                                href = string.Empty,
                                items = new[]
                                {
                                    new {
                                    id = string.Empty,
                                    name = string.Empty
                                    }
                                }
                            }
                        });

                    foreach (var item in json.albums.items)
                    {
                        result.Add(new AlbumViewModel()
                        {
                            Name = item.name,
                            GenreId = genre.Id,
                            Id = item.id
                        });
                    }
                }
            }

            foreach (var item in result)
            {
                var id = RandomId.NewId();
                var cmd = new CreateAlbumCommand()
                {
                    Id = id,
                    SpotifyId = item.Id,
                    GenreId = item.GenreId,
                    Name = item.Name
                };

                var rs = await handler.Handle(cmd);
                rows += rs.Rows;
            }

            return await Task.FromResult(new CommandResult(rows, ErrorCode.None));
        }

        public EventType GetEvent()
        {
            return EventType.None;
        }

        public class AlbumViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string GenreId { get; set; }
        }
    }
}
