using System;

namespace Cashback.Domain.Models
{
    public interface IGenre
    {
        Genre Build(string genre);
    }

    public abstract class Genre : IGenre
    {
        public Genre Build(string genre)
        {
            switch (genre)
            {
                case "pop": return new GenrePop();
                default:
                    break;
            }

            return null;
        }
        public abstract int GetCashback(DayOfWeek dayOfWeek);

        public string Id { get; set; }
        public string Name { get; set; }

    }

    public class GenrePop : Genre
    {
        public override int GetCashback(DayOfWeek dayOfWeek)
        {
            switch (dayOfWeek)
            {
                case DayOfWeek.Friday:
                    break;
                case DayOfWeek.Monday:
                    break;
                case DayOfWeek.Saturday:
                    break;
                case DayOfWeek.Sunday:
                    break;
                case DayOfWeek.Thursday:
                    break;
                case DayOfWeek.Tuesday:
                    break;
                case DayOfWeek.Wednesday:
                    break;
                default:
                    break;
            }

            return 0;
        }
    }
}
