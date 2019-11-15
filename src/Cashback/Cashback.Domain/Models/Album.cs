using System;

namespace Cashback.Domain.Models
{
    public class Album
    {
        public string Id { get; set; }
        public string SpotifyId { get; set; }
        public string Name { get; set; }
        public decimal Value { get { return Convert.ToDecimal(new Random().Next(1, 50)); } set { } }
        public Genre Genre { get; set; }
    }
}
