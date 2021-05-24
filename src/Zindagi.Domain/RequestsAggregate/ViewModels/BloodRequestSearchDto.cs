#nullable disable

namespace Zindagi.Domain.RequestsAggregate.ViewModels
{
    public class BloodRequestSearchRecordDto
    {
        public string SearchId { get; set; }
        public double SearchScore { get; set; }
        public long RequestId { get; set; }
        public string PatientName { get; set; }

        public string Reason { get; set; }

        public BloodDonationType DonationType { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public DetailedStatus Status { get; set; }

        public BloodRequestPriority Priority { get; set; }

        public double QuantityInUnits { get; set; }
    }
}
