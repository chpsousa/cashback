using Cashback.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Cashback.Domain.Models
{
    public class Genre : BaseModel
    {
        private List<Cashback> _cashbacks;
        public string Name { get; set; }
        public IReadOnlyCollection<Cashback> Cashbacks { get { return _cashbacks.AsReadOnly(); } }

        protected Genre()
        {
            _cashbacks = new List<Cashback>();
        }

        public Genre(string id): this()
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
        }

        public Genre(string id, string name): this()
        {
            this.Id = string.IsNullOrWhiteSpace(id) ? RandomId.NewId() : id;
            this.Name = name;
        }

        public void AddCashbackConfig(Cashback cashback)
        {
            if (cashback.Id == null)
                cashback.Id = RandomId.NewId();
            cashback.Genre = this;
            _cashbacks.Add(cashback);
        }

        public void AddCashbackConfigs(IEnumerable<Cashback> cashbacks)
        {
            foreach (var cashback in cashbacks)
                AddCashbackConfig(cashback);
        }

        public decimal GetCashback(DayOfWeek dayOfWeek)
        {
            if (this.Cashbacks == null || this.Cashbacks.Count() == 0)
                return 0;
            return this.Cashbacks.Where(w => w.DayOfWeek == dayOfWeek).FirstOrDefault().Percent;
        }
    }
}
