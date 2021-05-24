using System;
using System.Diagnostics;

namespace Zindagi.SeedWork
{
    [DebuggerDisplay("{GetPersistenceKey()}")]
    public readonly struct FullName
    {
        public FullName(string firstName, string? middleName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));

            FirstName = firstName.Trim();
            MiddleName = string.IsNullOrWhiteSpace(middleName) ? string.Empty : middleName.Trim();
            LastName = lastName.Trim();
        }

        public static FullName Create(string firstName, string? middleName, string lastName) => new(firstName, middleName, lastName);

        public string FirstName { get; }
        public string MiddleName { get; }
        public string LastName { get; }

        public override string ToString() => $"{FirstName} {MiddleName} {LastName}".Trim();
        public string GetPersistenceKey() => $"FULLNAME:{ToString()}";
    }
}
