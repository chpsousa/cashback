using Cashback.Domain.Commands;
using Cashback.Domain.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Cashback.Domain.Events
{
    public class EventsHandler
    {
        private static EventsHandler Instance { get; set; }
        private static readonly object LockObject = new object();

        private EventsHandler() { }
        public static EventsHandler GetInstance()
        {
            lock (LockObject)
            {
                if (Instance == null)
                    Instance = new EventsHandler();
            }
            return Instance;
        }

        EventListener[] GetListeners(EventType type)
        {
            return CashbackStartup.Listeners.Values
                .Where(l => l.Type.HasFlag(EventType.All) || l.Type.HasFlag(type))
                .ToArray();
        }

        public void Subscribe(EventListener listener)
        {
            if (CashbackStartup.Listeners.ContainsKey(listener.Id))
                throw new Exception("Duplicate event listener ID");
            CashbackStartup.Listeners[listener.Id] = listener;
        }

        public bool Unsubscribe(string id)
        {
            if (!CashbackStartup.Listeners.ContainsKey(id))
                return false;
            var listener = null as EventListener;
            return CashbackStartup.Listeners.TryRemove(id, out listener);
        }

        public async Task HandleEvent(CashbackDbContext context, Util.ICommand command, CommandResult result)
        {
            var ev = command.GetEvent();
            if (ev == EventType.None)
                return;

            var lstListeners = GetListeners(ev);
            foreach (var listener in lstListeners)
                await listener.Run(context, ev, command, result);
        }
    }
}
