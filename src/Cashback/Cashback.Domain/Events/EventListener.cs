using Cashback.Domain.Commands;
using Cashback.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cashback.Domain.Events
{
    public class EventListener
    {
        public string Id { get; protected set; }
        public EventType Type { get; }
        protected Func<CashbackDbContext, EventType, ICommand, CommandResult, Task> Method { get; set; }

        public EventListener(string id, EventType type, Func<CashbackDbContext, EventType, ICommand, CommandResult, Task> method = null)
        {
            this.Id = id ?? RandomId.NewId();
            this.Type = type;
            this.Method = method;
        }

        public virtual async Task Run(CashbackDbContext dbContext, EventType type, ICommand command, CommandResult result)
        {
            if (Method != null)
                await Method(dbContext, type, command, result);
        }
    }
}
