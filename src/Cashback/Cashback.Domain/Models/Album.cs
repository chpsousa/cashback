using Cashback.Domain.Util;
using System;

namespace Cashback.Domain.Models
{
    public class Album : BaseModel
    {
        public string SpotifyId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public Genre Genre { get; set; }
        public string GenreId { get { return this.Genre.Id; } set { this.GenreId = this.Genre.Id; } }

        public Album(string id, string spotifyId, string name, Genre genre)
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
            this.SpotifyId = spotifyId;
            this.Name = name;
            this.Genre = genre;
            this.Value = Convert.ToDecimal(new Random().Next(1, 50));
        }
    }
}
