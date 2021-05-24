using System.Collections.Generic;
using MediatR;
using Zindagi.Domain.RequestsAggregate.ViewModels;

namespace Zindagi.Domain.RequestsAggregate.Queries
{
    public class GetBloodRequests : IRequest<List<BloodRequestDto>>
    {
        public GetBloodRequests(long userId) => UserId = userId;

        public long UserId { get; private set; }
    }
}
