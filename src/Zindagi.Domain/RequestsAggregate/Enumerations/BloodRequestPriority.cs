using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate
{
    public sealed class BloodRequestPriority : Enumeration
    {
        public static readonly BloodRequestPriority None = new(0, "-SELECT-");
        public static readonly BloodRequestPriority Emergency = new(1, "Emergency (Immediate)");
        public static readonly BloodRequestPriority High = new(2, "High (Two Hours)");
        public static readonly BloodRequestPriority Medium = new(3, "Medium (Six Hours)");
        public static readonly BloodRequestPriority Low = new(4, "Low (One Day or More)");

        private BloodRequestPriority(int id, string name) : base(id, name) { }
    }
}
