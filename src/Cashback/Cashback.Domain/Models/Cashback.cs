using System;

namespace Cashback.Domain.Models
{
    public class Cashback
    {
        public Genre Genre { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public decimal Percent { get; set; }
    }
}
