using System;
using System.Collections.Generic;

namespace Cashback.Domain.Models
{
    public class Sale
    {
        public string Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string CustomerName { get; set; }
        public IEnumerable<SaleItem> Albums { get; set; }
        public decimal TotalValue { get; set; }
        public decimal TotalCashback { get; set; }
    }
}
