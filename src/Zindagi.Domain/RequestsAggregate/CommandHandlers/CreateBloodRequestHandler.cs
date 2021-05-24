using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Zindagi.Domain.RequestsAggregate.Commands;
using Zindagi.Domain.RequestsAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate.CommandHandlers
{
    public class CreateBloodRequestHandler : IRequestHandler<CreateBloodRequest, Result<BloodRequestDto>>
    {
        private readonly IMapper _mapper;
        private readonly IBloodRequestRepository _bloodRequestRepository;

        public CreateBloodRequestHandler(IMapper mapper, IBloodRequestRepository repository, IMediator mediator)
        {
            _mapper = mapper;
            _bloodRequestRepository = repository;
        }

        public async Task<Result<BloodRequestDto>> Handle(CreateBloodRequest request, CancellationToken cancellationToken)
        {
            var result = await _bloodRequestRepository.CreateAsync(BloodRequest.Create(request), cancellationToken);
            await _bloodRequestRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Result<BloodRequestDto>.Success(_mapper.Map<BloodRequestDto>(result));
        }
    }
}
