using MediatR;

namespace Zindagi.Domain.RequestsAggregate.Commands
{
    public class AcceptBloodDonationRequest : IRequest<bool>
    {
        public AcceptBloodDonationRequest(long requestId, long userId)
        {
            RequestId = requestId;
            UserId = userId;
        }

        public long RequestId { get; set; }
        public long UserId { get; set; }
    }
}
