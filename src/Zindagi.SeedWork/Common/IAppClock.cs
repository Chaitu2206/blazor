using System;
using NodaTime;

namespace Zindagi.SeedWork
{
    public interface IAppClock
    {
        DateTime GetDateTimeNow();
        Instant GetInstant();
    }
}
