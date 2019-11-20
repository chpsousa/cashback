using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Cashback.Domain.Commands.Genres;
using Cashback.Domain.Events;
using Cashback.Domain.Models;
using Cashback.Domain.Util;
using Newtonsoft.Json;

namespace Cashback.Domain.Commands.Spotify
{
    public class PopulateGenresCommand : SpotifyCommand, ICommand
    {
        public async Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            if (handler.DbContext.Genres != null && handler.DbContext.Genres.Count() > 0)
                return await Task.FromResult(new CommandResult(ErrorCode.None));
            if (string.IsNullOrEmpty(AccessToken))
                Authorize();

            IList<GenreViewModel> result = new List<GenreViewModel>();
            var defaultCashback = new DefaultCashback();

            HttpClient httpClient = new HttpClient();
            var genresUrls = new[] {
                "browse/categories/pop?country=BR",
                "browse/categories/mpb?country=BR",
                "browse/categories/classical?country=BR",
                "browse/categories/rock?country=BR"
            };

            HttpResponseMessage response = null;
            foreach (var url in genresUrls)
            {
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);
                response = await httpClient.GetAsync("https://api.spotify.com/v1/" + url);
                var json = JsonConvert.DeserializeObject<GenreViewModel>(await response.Content.ReadAsStringAsync());

                if (response.IsSuccessStatusCode && !result.Any(c => c.Name == json.Name))
                    result.Add(json);
            }

            foreach (var item in result)
            {
                var id = RandomId.NewId();
                var cashback = new List<CashbackCommand>();
                var lst = defaultCashback.GetDefaultCashback(id, item.Name);
                lst.ForEach(f => cashback.Add(new CashbackCommand() { Id = f.Id, GenreId = f.GenreId, DayOfWeek = f.DayOfWeek, Percent = f.Percent }));

                var cmd = new CreateGenreCommand()
                {
                    Id = id,
                    Name = item.Name,
                    Cashback = cashback
                };

                await handler.Handle(cmd);
            }

            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public EventType GetEvent()
        {
            return EventType.None;
        }

        public class GenreViewModel
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
