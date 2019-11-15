using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashback.Domain.Models
{
    public class Genre
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Cashback> Cashbacks { get; set; }

        public decimal GetCashback(DayOfWeek dayOfWeek)
        {
            return this.Cashbacks.Where(w => w.DayOfWeek == dayOfWeek).FirstOrDefault().Percent;
        }
    }
}
