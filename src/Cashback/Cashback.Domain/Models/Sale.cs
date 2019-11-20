using Cashback.Domain.Models.Interfaces;
using Cashback.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashback.Domain.Models
{
    public class Sale : BaseModel, ISale
    {
        private List<SaleItem> _items;
        public DateTimeOffset Date { get; set; }
        public string CustomerName { get; set; }
        public IReadOnlyCollection<SaleItem> Items { get { return _items.AsReadOnly(); } }
        public decimal TotalValue { get; set; }
        public decimal TotalCashback { get; set; }

        protected Sale()
        {
            _items = new List<SaleItem>();
            Date = DateTimeOffset.Now;
        }

        public Sale(string id) : this()
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
        }

        public Sale(
            string id,
            string customerName
        ) : this()
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
            this.CustomerName = customerName;
        }

        public void AddItem(SaleItem item)
        {
            if (item.Id == null)
                item.Id = RandomId.NewId();
            item.Sale = this;
            _items.Add(item);
        }

        public void AddItems(IEnumerable<SaleItem> collection)
        {
            foreach (var item in collection)
                AddItem(item);
        }
    }
}
