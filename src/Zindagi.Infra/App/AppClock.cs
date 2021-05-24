using System;
using NodaTime;
using Zindagi.SeedWork;

namespace Zindagi.Infra
{
    public class AppClock : IAppClock
    {
        public DateTime GetDateTimeNow() => DateTime.UtcNow;

        public Instant GetInstant() => SystemClock.Instance.GetCurrentInstant();
    }
}
