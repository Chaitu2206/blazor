using MediatR;
using Zindagi.Domain.UserAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate.Queries
{
    public class GetUser : IRequest<Result<UserDto>>
    {
        public GetUser(long userId) => UserId = userId;

        public long UserId { get; set; }
    }
}
