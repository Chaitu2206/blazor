using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.RequestsAggregate.Queries;
using Zindagi.Domain.RequestsAggregate.ViewModels;

namespace Zindagi.Domain.RequestsAggregate.QueryHandlers
{
    public class GetBloodRequestsHandler : IRequestHandler<GetBloodRequests, List<BloodRequestDto>>
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;

        public GetBloodRequestsHandler(IBloodRequestRepository bloodRequestRepository) =>
            _bloodRequestRepository = bloodRequestRepository;

        public async Task<List<BloodRequestDto>> Handle(GetBloodRequests request, CancellationToken cancellationToken) =>
            await _bloodRequestRepository.GetAcceptedBloodRequests(request.UserId, cancellationToken);
    }
}
