using Cashback.Domain.Commands;
using Cashback.Domain.Commands.Spotify;
using Cashback.Domain.Queries;
using Cashback.Domain.Queries.Albums;
using Cashback.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Cashback.API.v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly CashbackCommandsHandler _commandsHandler;
        private readonly CashbackQueriesHandler _queriesHandler;

        public AlbumsController(CashbackCommandsHandler commandsHandler, CashbackQueriesHandler queriesHandler)
        {
            _commandsHandler = commandsHandler;
            _queriesHandler = queriesHandler;
        }

        [HttpGet]
        public async Task<ActionResult<CommandResult>> Get()
        {
            await _commandsHandler.Handle(new PopulateAlbumCommand());
            var list = await _queriesHandler.RunQuery(new ListAlbumsQuery());
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CommandResult>> GetById(string id)
        {
            await _commandsHandler.Handle(new PopulateAlbumCommand());
            var list = await _queriesHandler.RunQuery(new GetAlbumQuery() { Id = id });
            return Ok(list?.FirstOrDefault());
        }


    }
}
