using Cashback.Domain.Commands;
using Cashback.Domain.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Cashback.API.v1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly CashbackCommandsHandler _commandsHandler;
        private readonly CashbackQueriesHandler _queriesHandler;

        public ValuesController(CashbackCommandsHandler commandsHandler, CashbackQueriesHandler queriesHandler)
        {
            _commandsHandler = commandsHandler;
            _queriesHandler = queriesHandler;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get()
        {
            var serverIp = await GetPublicIpAsync();
            var clientIp = Request.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var dbConnectionResult = await _commandsHandler.DbContext.TryConnectionAsync();
            var dbName = _commandsHandler.DbContext.Database.ProviderName;

            var apiBuildDate = $"{string.Format("{0:yyyy-MM-dd HH:mm}", await GetLinkerTimeAsync("Cashback.API"))} UTC";

            return $@"Cashback.API (v1)

Environment........: {Startup.Environment}
Server IP..........: {serverIp}
Client IP..........: {clientIp}

DB Connection.: {dbName} {(dbConnectionResult.CanConnect ? "(Ok)" : (string.IsNullOrEmpty(dbConnectionResult.ErrorMessage) ? "(Error)" : "- Error: " + dbConnectionResult.ErrorMessage))}

Build Dates
API................: {apiBuildDate}
";
        }

        async Task<DateTime> GetLinkerTimeAsync(string name, TimeZoneInfo target = null) =>
           await Task.Run(() =>
           {
               if (string.IsNullOrEmpty(name))
                   name = Assembly.GetExecutingAssembly().GetName().Name;
               var _assembly = AppDomain.CurrentDomain.GetAssemblies()
                   .SingleOrDefault(assembly => assembly.GetName().Name == name);
               if (_assembly == null)
                   throw new Exception($"Assembly {name} not found!");
               var filePath = _assembly.Location;
               const int c_PeHeaderOffset = 60;
               const int c_LinkerTimestampOffset = 8;
               var buffer = new byte[2048];
               using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                   stream.Read(buffer, 0, 2048);
               var offset = BitConverter.ToInt32(buffer, c_PeHeaderOffset);
               var secondsSince1970 = BitConverter.ToInt32(buffer, offset + c_LinkerTimestampOffset);
               var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
               var linkTimeUtc = epoch.AddSeconds(secondsSince1970);
               var tz = target ?? TimeZoneInfo.Local;
               var localTime = TimeZoneInfo.ConvertTimeFromUtc(linkTimeUtc, tz);
               return localTime;
           });

        async Task<string> GetPublicIpAsync()
        {
            try
            {
                using (var wc = new WebClient())
                {
                    string str = await wc.DownloadStringTaskAsync("http://checkip.dyndns.org/");
                    var m = Regex.Match(str, @"\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}");
                    return m?.Value;
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}
