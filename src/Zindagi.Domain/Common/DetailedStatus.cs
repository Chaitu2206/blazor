using Zindagi.SeedWork;

namespace Zindagi.Domain
{
    public sealed class DetailedStatus : Enumeration
    {
        public static readonly DetailedStatus None = new(0, "-SELECT-");
        public static readonly DetailedStatus Open = new(1, "OPEN");
        public static readonly DetailedStatus Assigned = new(2, "ASSIGNED");
        public static readonly DetailedStatus Cancelled = new(3, "CANCELLED");
        public static readonly DetailedStatus Duplicate = new(4, "DUPLICATE");
        public static readonly DetailedStatus Rejected = new(5, "REJECTED");
        public static readonly DetailedStatus Fulfilled = new(6, "FULFILLED");

        private DetailedStatus(int id, string name) : base(id, name) { }
    }
}