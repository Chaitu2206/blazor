using Zindagi.Domain.RequestsAggregate.Commands;
using Zindagi.Domain.RequestsAggregate.DomainEvents;
using Zindagi.Domain.UserAggregate;
using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate
{
    public class BloodRequest : AggregateBase
    {
#pragma warning disable 8618
        protected BloodRequest() { }
#pragma warning restore 8618
        private BloodRequest(long requestorId, string patientName, string reason, double quantityInUnits, BloodDonationType donationType, BloodGroup bloodGroup, BloodRequestPriority priority, DetailedStatus status) : this()
        {
            RequestorId = requestorId;
            PatientName = patientName;
            Reason = reason;
            QuantityInUnits = quantityInUnits;
            DonationType = donationType;
            BloodGroup = bloodGroup;
            Priority = priority;
            Status = status;
        }

        public double QuantityInUnits { get; set; }
        public double QuantityInMl => QuantityInUnits * 450.00;
        public string PatientName { get; set; }
        public string Reason { get; set; }
        public BloodDonationType DonationType { get; set; }
        public BloodGroup BloodGroup { get; set; }
        public BloodRequestPriority Priority { get; set; }
        public DetailedStatus Status { get; set; }


        public long RequestorId { get; private set; }
        public virtual User Requestor { get; set; } = null!;

        public long? AssigneeId { get; private set; }
        public virtual User? Assignee { get; set; }


        public void SetRequestor(long requestorId) => RequestorId = requestorId;
        public void SetAssignee(long assigneeId) => AssigneeId = assigneeId;
        public void CancelRequest() => Status = DetailedStatus.Cancelled;

        public static BloodRequest Create(CreateBloodRequest request)
        {
            var result = new BloodRequest(request.RequestorId,
                                          request.PatientName,
                                          request.Reason,
                                          request.QuantityInUnits,
                                          Enumeration.FromValue<BloodDonationType>(request.DonationType),
                                          Enumeration.FromValue<BloodGroup>(request.BloodGroup),
                                          Enumeration.FromValue<BloodRequestPriority>(request.Priority),
                                          DetailedStatus.Open);
            result.AddDomainEvent(new BloodRequestCreated(result));
            return result;
        }
    }
}
