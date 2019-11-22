using Cashback.Domain.Commands;
using Cashback.Domain.Commands.Sales;
using Cashback.Domain.Queries;
using Cashback.Domain.Queries.Sales;
using Cashback.Domain.Util;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Cashback.API.v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly CashbackCommandsHandler _commandsHandler;
        private readonly CashbackQueriesHandler _queriesHandler;

        public SalesController(CashbackCommandsHandler commandsHandler, CashbackQueriesHandler queriesHandler)
        {
            _commandsHandler = commandsHandler;
            _queriesHandler = queriesHandler;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CommandResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommandResult>> Post([FromBody] CreateSaleCommand cmd)
        {
            var result = await _commandsHandler.Handle(cmd);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(SaleViewModel[]), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommandResult>> Get()
        {
            var list = await _queriesHandler.RunQuery(new ListSalesQuery());
            return Ok(list);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SaleViewModel), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CommandResult>> GetById(string id)
        {
            var list = await _queriesHandler.RunQuery(new GetSaleQuery() { Id = id });
            return Ok(list?.FirstOrDefault());
        }
    }
}
