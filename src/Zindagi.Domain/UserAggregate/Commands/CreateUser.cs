using MediatR;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.Commands
{
    public class CreateUser : IRequest<Result<UserDto>>
    {
        public CreateUser(OpenIdUser openIdUser) => OpenIdUser = openIdUser;

        public OpenIdUser OpenIdUser { get; set; }
    }
}
