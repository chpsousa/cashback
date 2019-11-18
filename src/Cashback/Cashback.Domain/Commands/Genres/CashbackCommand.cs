using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Commands.Genres
{
    public class CashbackCommand
    {
        public string Id { get; set; }
        public string GenreId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public decimal Percent { get; set; }
    }
}
