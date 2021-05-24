using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.RequestsAggregate.Commands;
using Zindagi.Domain.UserAggregate;

namespace Zindagi.Domain.RequestsAggregate.CommandHandlers
{
    public class AcceptBloodDonationRequestHandler : IRequestHandler<AcceptBloodDonationRequest, bool>
    {
        private readonly IBloodRequestRepository _bloodRequestRepository;
        private readonly IUserRepository _userRepository;

        public AcceptBloodDonationRequestHandler(IBloodRequestRepository bloodRequestRepository, IUserRepository userRepository)
        {
            _bloodRequestRepository = bloodRequestRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(AcceptBloodDonationRequest request, CancellationToken cancellationToken)
        {
            var requestInfo = await _bloodRequestRepository.GetAsync(request.RequestId, cancellationToken);
            requestInfo.Status = DetailedStatus.Assigned;
            requestInfo.Assignee = await _userRepository.GetAsync(request.UserId, cancellationToken);

            await _bloodRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            var result = await _bloodRequestRepository.UpdateAsync(requestInfo, cancellationToken);
            return result;
        }
    }
}
