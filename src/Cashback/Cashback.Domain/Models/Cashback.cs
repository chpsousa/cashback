using Cashback.Domain.Util;
using System;

namespace Cashback.Domain.Models
{
    public class Cashback : BaseModel
    {
        public Genre Genre { get; set; }
        public string GenreId { get { return this.Genre.Id; } set { this.GenreId = this.Genre.Id; } }
        public DayOfWeek DayOfWeek { get; set; }
        public decimal Percent { get; set; }

        public Cashback(
            string id,
            string genreId,
            DayOfWeek dayOfWeek,
            decimal percent
        )
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
            this.Genre = new Genre(genreId);
            this.DayOfWeek = dayOfWeek;
            this.Percent = percent;
        }
    }
}
