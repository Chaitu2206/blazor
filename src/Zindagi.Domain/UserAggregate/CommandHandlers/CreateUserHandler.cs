using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Zindagi.Domain.UserAggregate.Commands;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.CommandHandlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Result<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<Result<UserDto>> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.CreateAsync(User.Create(request.OpenIdUser, false), cancellationToken);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return user is null! ? Result<UserDto>.Error("Failed to create user") : Result<UserDto>.Success(user.GetDto());
        }
    }
}
