using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate
{
    public sealed class BloodDonationType : Enumeration
    {
        public static readonly BloodDonationType None = new(0, "-SELECT-");

        public static readonly BloodDonationType WholeBloodDonation = new(1, "Whole Blood Donation");
        public static readonly BloodDonationType PlasmaDonation = new(2, "Plasma Donation (Apheresis)");
        public static readonly BloodDonationType PlateletDonation = new(3, "Platelet Donation (Plateletpheresis)");

        private BloodDonationType(int id, string name) : base(id, name) { }
    }
}
