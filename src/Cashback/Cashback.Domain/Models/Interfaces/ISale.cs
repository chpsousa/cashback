using System.Collections.Generic;

namespace Cashback.Domain.Models.Interfaces
{
    public interface ISale
    {
        void AddItem(SaleItem item);
        void AddItems(IEnumerable<SaleItem> collection);
    }
}
