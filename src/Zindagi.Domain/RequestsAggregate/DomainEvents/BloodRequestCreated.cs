using MediatR;

namespace Zindagi.Domain.RequestsAggregate.DomainEvents
{
    public class BloodRequestCreated : INotification
    {
        public BloodRequestCreated(BloodRequest request) => Request = request;

        public BloodRequest Request { get; }
    }
}
