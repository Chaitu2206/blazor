using System;
using System.Collections.Generic;

#nullable disable

namespace Zindagi.SeedWork
{
    [Serializable]
    public abstract class SimpleValueObject<T> : ValueObject
    {
        protected SimpleValueObject(T value) => Value = value;

        public T Value { get; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString() => Value?.ToString() ?? string.Empty;

        public static implicit operator T(SimpleValueObject<T> valueObject) => valueObject.Value;
    }
}
