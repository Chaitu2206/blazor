using Zindagi.SeedWork;

namespace Zindagi.Domain
{
    public sealed class Status : Enumeration
    {
        public static readonly Status None = new(0, "-SELECT-");

        public static readonly Status Active = new(1, "ACTIVE");
        public static readonly Status Inactive = new(2, "IN ACTIVE");

        public Status(int id, string name) : base(id, name) { }
    }
}