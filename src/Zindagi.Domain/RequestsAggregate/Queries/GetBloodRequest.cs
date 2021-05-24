using MediatR;
using Zindagi.Domain.RequestsAggregate.ViewModels;

namespace Zindagi.Domain.RequestsAggregate.Queries
{
    public class GetBloodRequest : IRequest<BloodRequestDto>
    {
        public GetBloodRequest(long requestId) => RequestId = requestId;
        public long RequestId { get; private set; }
    }
}
