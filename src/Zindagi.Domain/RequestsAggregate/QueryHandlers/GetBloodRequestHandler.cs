using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.RequestsAggregate.Queries;
using Zindagi.Domain.RequestsAggregate.ViewModels;

namespace Zindagi.Domain.RequestsAggregate.QueryHandlers
{
    public class GetBloodRequestHandler : IRequestHandler<GetBloodRequest, BloodRequestDto>
    {
        private readonly IBloodRequestRepository _repository;

        public GetBloodRequestHandler(IBloodRequestRepository repository) =>
            _repository = repository;

        public async Task<BloodRequestDto> Handle(GetBloodRequest request, CancellationToken cancellationToken) =>
            await _repository.GetBloodRequest(request.RequestId, cancellationToken);
    }
}
