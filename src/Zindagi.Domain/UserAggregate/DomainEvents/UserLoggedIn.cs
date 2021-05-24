using System.Security.Claims;
using MediatR;

namespace Zindagi.Domain.UserAggregate.DomainEvents
{
    public class UserLoggedIn : INotification
    {
        public UserLoggedIn(ClaimsPrincipal claimsPrincipal) => ClaimsPrincipal = claimsPrincipal;

        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}
