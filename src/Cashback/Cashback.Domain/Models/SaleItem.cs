namespace Cashback.Domain.Models
{
    public class SaleItem : BaseModel
    {
        public Sale Sale { get; set; }
        public string SaleId { get { return this.Sale.Id; } set { this.SaleId = this.Sale.Id; } }
        public Album Album { get; set; }
        public string AlbumId { get { return this.Album.Id; } set { this.AlbumId = this.Album.Id; } }
        public decimal CashbackValue { get { return this.Album.Value * (this.Album.Genre.GetCashback(this.Sale.Date.DayOfWeek) / 100); } }

        public SaleItem(Sale sale, Album album)
        {
            this.Sale = sale;
            this.Album = album;
        }
    }
}
