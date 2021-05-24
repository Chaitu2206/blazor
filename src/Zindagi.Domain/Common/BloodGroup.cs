using Zindagi.SeedWork;

namespace Zindagi.Domain
{
    public sealed class BloodGroup : Enumeration
    {
        public static readonly BloodGroup None = new(0, "-SELECT-");

        public static readonly BloodGroup APositive = new(1, "A +VE");
        public static readonly BloodGroup ANegative = new(2, "A -VE");

        public static readonly BloodGroup BPositive = new(3, "B +VE");
        public static readonly BloodGroup BNegative = new(4, "B -VE");

        public static readonly BloodGroup OPositive = new(5, "O +VE");
        public static readonly BloodGroup ONegative = new(6, "O -VE");

        public static readonly BloodGroup AbPositive = new(7, "AB +VE");
        public static readonly BloodGroup AbNegative = new(8, "AB -VE");

        private BloodGroup(int id, string name) : base(id, name) { }
    }
}
