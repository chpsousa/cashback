using System.Linq;
using System.Threading.Tasks;
using Cashback.Domain.Events;
using Cashback.Domain.Models;
using Cashback.Domain.Util;
using Microsoft.EntityFrameworkCore;

namespace Cashback.Domain.Commands.Sales
{
    public class CreateSaleCommand : ICommand
    {
        public string Id { get; set; }
        public string CustomerName { get; set; }
        public SaleItemCommand[] Items { get; set; }

        public async Task<CommandResult> GetErrorAsync(CashbackCommandsHandler handler)
        {
            if (Items == null || Items.Length == 0)
                return await Task.FromResult(new CommandResult(ErrorCode.InvalidParameters, "There are no items for this sale"));
            if (string.IsNullOrWhiteSpace(CustomerName))
                return await Task.FromResult(new CommandResult(ErrorCode.InvalidParameters, "Parameter customerName is required"));
            return await Task.FromResult(new CommandResult(ErrorCode.None));
        }

        public async Task<CommandResult> ExecuteAsync(CashbackCommandsHandler handler)
        {
            var obj = new Sale(Id, CustomerName);
            foreach (var item in Items)
            {
                var album = await handler.DbContext
                    .Albums
                    .Include(i => i.Genre)
                    .ThenInclude(t => t.Cashbacks)
                    .Where(w => w.Id == item.AlbumId)
                    .FirstOrDefaultAsync();

                if (album == null)
                    return await Task.FromResult(new CommandResult(ErrorCode.NotFound, $"Album with id {item.AlbumId} was not found"));
                var saleItem = new SaleItem(obj.Id, album.Id);
                saleItem.Sale = obj;
                saleItem.Album = album;
                saleItem.CalcCashback();
                obj.AddItem(saleItem);
            }

            obj.TotalValue = obj.Items.Sum(s => s.Album.Value);
            obj.TotalCashback = obj.Items.Sum(s => s.CashbackValue);

            await handler.DbContext.Sales.AddAsync(obj);
            await handler.DbContext.SaleItems.AddRangeAsync(obj.Items);
            var rows = await handler.DbContext.SaveChangesAsync();

            return await Task.FromResult(new CommandResult(rows, ErrorCode.None));
        }

        public EventType GetEvent()
        {
            return EventType.None;
        }
    }

    public class SaleItemCommand
    {
        public string AlbumId { get; set; }
    }
}
