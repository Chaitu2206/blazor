using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Zindagi.Domain.UserAggregate.DomainEvents;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.DomainEventsHandler
{
    public class UserLoggedInHandler : INotificationHandler<UserLoggedIn>
    {
        private readonly ILogger<UserLoggedInHandler> _logger;
        private readonly IUserRepository _userRepository;

        public UserLoggedInHandler(IUserRepository userRepository, ILogger<UserLoggedInHandler> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task Handle(UserLoggedIn notification, CancellationToken cancellationToken)
        {
            var openIdUser = OpenIdUser.Create(notification.ClaimsPrincipal);
            if (openIdUser is null)
                return;

            var result = await _userRepository.RegisterUserLoginAsync(User.Create(openIdUser, true), cancellationToken);
            await _userRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            _logger.LogDebug("[User] [Login] {user}", result);
        }
    }
}
