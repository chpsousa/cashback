namespace Cashback.Domain.Models
{
    public class SaleItem
    {
        public Sale Sale { get; set; }
        public Album Album { get; set; }
        public decimal CashbackValue { get; set; }
    }
}
