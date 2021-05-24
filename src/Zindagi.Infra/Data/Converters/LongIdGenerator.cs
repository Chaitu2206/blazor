using System;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Zindagi.Infra.Data.Converters
{
    public class LongIdGenerator : ValueGenerator<long>
    {
        public override long Next(EntityEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            return DateTime.UtcNow.Ticks;
        }

        public override bool GeneratesTemporaryValues { get; }
    }
}