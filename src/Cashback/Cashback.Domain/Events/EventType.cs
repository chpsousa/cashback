using System;

namespace Cashback.Domain.Events
{
    [Flags]
    public enum EventType
    {
        None = 0,
        All = 1
    }
}
