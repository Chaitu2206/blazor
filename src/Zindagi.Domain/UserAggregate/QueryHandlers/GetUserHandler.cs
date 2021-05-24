using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.UserAggregate.Queries;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.QueryHandlers
{
    public class GetUserHandler : IRequestHandler<GetUser, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result<UserDto>> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var result = await _userRepository.GetAsync(request.UserId, cancellationToken);
            return Result<UserDto>.Success(result.GetDto());
        }
    }
}
