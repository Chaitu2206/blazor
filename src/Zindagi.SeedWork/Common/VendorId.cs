using System;
using System.Diagnostics;

namespace Zindagi.SeedWork
{
    [DebuggerDisplay("{GetPersistenceKey()}")]
    public readonly struct VendorId : IEquatable<VendorId>
    {
        public VendorId(string value) => Value = string.IsNullOrWhiteSpace(value) ? string.Empty : value.Trim().ToUpperInvariant();
        public static VendorId Create(string key) => new(key);
        public string Value { get; }

        public override string ToString() => Value;

        public string GetPersistenceKey() => $"OPENID:{Value}";
        public bool HasValue => Value != string.Empty;

        public bool Equals(VendorId other) => string.Equals(other.Value, Value, StringComparison.Ordinal);
        public override bool Equals(object? obj)
        {
            if (obj is null)
                return false;
            return obj is VendorId id && Equals(id);
        }

        public override int GetHashCode() => Value.GetHashCode(StringComparison.Ordinal);

        public static bool operator ==(VendorId v1, VendorId v2) => v1.Equals(v2);

        public static bool operator !=(VendorId v1, VendorId v2) => !v1.Equals(v2);
    }
}
