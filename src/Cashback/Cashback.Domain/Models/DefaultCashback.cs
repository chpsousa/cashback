using System;
using System.Collections.Generic;

namespace Cashback.Domain.Models
{
    public class DefaultCashback
    {
        public List<Cashback> GetDefaultCashback(string genreId, string genreName)
        {
            var defaultCashback = new List<Cashback>();

            switch (genreName)
            {
                case "Pop":
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Sunday, 25));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Monday, 7));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Tuesday, 6));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Wednesday, 2));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Thursday, 10));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Friday, 15));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Saturday, 20));
                    break;

                case "MPB":
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Sunday, 30));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Monday, 5));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Tuesday, 10));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Wednesday, 15));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Thursday, 20));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Friday, 25));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Saturday, 30));
                    break;

                case "Rock":
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Sunday, 35));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Monday, 3));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Tuesday, 5));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Wednesday, 8));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Thursday, 13));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Friday, 18));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Saturday, 25));
                    break;

                case "Classical":
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Sunday, 40));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Monday, 10));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Tuesday, 15));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Wednesday, 15));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Thursday, 15));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Friday, 20));
                    defaultCashback.Add(new Cashback(null, genreId, DayOfWeek.Saturday, 40));
                    break;
            }

            return defaultCashback;
        }
    } 
}
