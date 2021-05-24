using MediatR;

namespace Zindagi.Domain.RequestsAggregate.Commands
{
    public class CancelBloodDonationRequest : IRequest<bool>
    {
        public CancelBloodDonationRequest(long requestId, long cancelledBy)
        {
            RequestId = requestId;
            CancelledBy = cancelledBy;
        }
        public long RequestId { get; set; }
        public long CancelledBy { get; set; }
    }
}
