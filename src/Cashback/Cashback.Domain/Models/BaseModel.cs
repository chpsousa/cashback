using System;
using System.Collections.Generic;
using System.Text;

namespace Cashback.Domain.Models
{
    public abstract class BaseEntity
    {
        public bool Removed { get; set; }

        public virtual void Remove()
        {
            Removed = true;
        }
    }
}
