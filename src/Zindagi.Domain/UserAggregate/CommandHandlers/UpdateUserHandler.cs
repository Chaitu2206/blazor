using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.UserAggregate.Commands;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.CommandHandlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUser, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result<UserDto>> Handle(UpdateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(request.UserId, cancellationToken);
            user.Update(request);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return Result<UserDto>.Success(user.GetDto());
        }
    }
}
