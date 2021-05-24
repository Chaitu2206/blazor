using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.RequestsAggregate.Commands;

namespace Zindagi.Domain.RequestsAggregate.CommandHandlers
{
    public class CancelBloodDonationRequestHandler : IRequestHandler<CancelBloodDonationRequest, bool>
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;

        public CancelBloodDonationRequestHandler(IBloodRequestRepository bloodRequestRepository) => _bloodRequestRepository = bloodRequestRepository;

        public async Task<bool> Handle(CancelBloodDonationRequest request, CancellationToken cancellationToken)
        {
            var requestInfo = await _bloodRequestRepository.GetAsync(request.RequestId, cancellationToken);
            requestInfo.CancelRequest();

            var result = await _bloodRequestRepository.UpdateAsync(requestInfo, cancellationToken);
            await _bloodRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return result;
        }
    }
}
