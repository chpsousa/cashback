namespace Cashback.Domain.Models
{
    public class SaleItem
    {
        public Sale Sale { get; set; }
        public Album Album { get; set; }
        public decimal CashbackValue { get { return this.Album.Value * (this.Album.Genre.GetCashback(this.Sale.Date.DayOfWeek) / 100); } }
    }
}
