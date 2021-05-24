using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Zindagi.Domain.RequestsAggregate.ViewModels;
using Zindagi.SeedWork;

namespace Zindagi.Domain.RequestsAggregate
{
    public interface IBloodRequestRepository : IRepository<BloodRequest>
    {
        Task<BloodRequest> CreateAsync(BloodRequest request, CancellationToken ct = default);
        Task<BloodRequest> GetAsync(long id, CancellationToken ct = default);
        Task<bool> UpdateAsync(BloodRequest request, CancellationToken ct = default);
        Task<BloodRequestDto> GetBloodRequest(long requestId, CancellationToken ct = default);
        Task<List<BloodRequestDto>> GetBloodRequests(long userId, CancellationToken ct = default);
        Task<List<BloodRequestDto>> GetAcceptedBloodRequests(long userId, CancellationToken ct = default);
        Task<List<BloodRequestDto>> GetWaitingBloodRequests(CancellationToken ct = default);
        Task<List<BloodRequestDto>> GetWaitingBloodRequests(long userId, CancellationToken ct = default);
    }
}
