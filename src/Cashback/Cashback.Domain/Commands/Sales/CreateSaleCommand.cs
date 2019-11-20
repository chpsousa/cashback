using System.Threading.Tasks;
using Cashback.Domain.Events;
using Cashback.Domain.Util;

namespace Cashback.Domain.Commands.Sales
{
    public class CreateSaleCommand : ICommand
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public SaleItemCommand[] Items { get; set; }

        public Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public EventType GetEvent()
        {
            throw new System.NotImplementedException();
        }
    }

    public class SaleItemCommand
    {
        public string AlbumId { get; set; }
    }
}
