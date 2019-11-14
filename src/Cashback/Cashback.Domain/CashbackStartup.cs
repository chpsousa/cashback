using Cashback.Domain.Events;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;

namespace Cashback.Domain
{
    public static class CashbackStartup
    {
        internal static string CashbackConnectionString { get; private set; }

        internal static ConcurrentDictionary<string, EventListener> Listeners { get; set; }

        static CashbackStartup()
        {
            Listeners = new ConcurrentDictionary<string, EventListener>();
        }

        public static async void Configure(string cashbackConnStr)
        {
            CashbackConnectionString = cashbackConnStr;

            Listeners = GetListeners();
        }

        static ConcurrentDictionary<string, EventListener> GetListeners()
        {
            var type = typeof(EventListener);
            var lstTypes = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => type.FullName != t.FullName && type.IsAssignableFrom(t))
                .ToArray();
            var lst = new ConcurrentDictionary<string, EventListener>();
            foreach (var t in lstTypes)
            {
                var listener = Activator.CreateInstance(t) as EventListener;
                lst[listener.Id] = listener;
            }
            return lst;
        }
    }
}
