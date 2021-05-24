using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using MimeKit;
using Zindagi.Domain.RequestsAggregate.DomainEvents;
using Zindagi.Domain.UserAggregate;
using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate.DomainEventsHandler
{
    public class BloodRequestCreatedHandler : INotificationHandler<BloodRequestCreated>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessaging _messaging;

        public BloodRequestCreatedHandler(IMessaging messaging, IUserRepository userRepository)
        {
            _messaging = messaging;
            _userRepository = userRepository;
        }

        public async Task Handle(BloodRequestCreated notification, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetAsync(notification.Request.RequestorId, cancellationToken);
            await _messaging.SendEmail(new List<MailboxAddress> { new(user.FullName, user.Email) },
                                 "New Request Created [Blood]", $"Request for blood is created.<br/> Request ID: {notification.Request.Id}");
        }
    }
}
