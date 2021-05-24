#nullable disable

namespace Zindagi.Domain.RequestsAggregate.ViewModels
{
    public record BloodRequestDto
    {
        public long Id { get; init; }
        public long RequestorId { get; init; }
        public long? AssigneeId { get; init; }
        public string PatientName { get; init; }
        public string Reason { get; init; }
        public BloodDonationType DonationType { get; init; }
        public BloodGroup BloodGroup { get; init; }
        public DetailedStatus Status { get; init; }
        public BloodRequestPriority Priority { get; init; }
        public double QuantityInUnits { get; init; }
    }
}
