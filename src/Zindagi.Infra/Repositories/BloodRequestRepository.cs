using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zindagi.Domain;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Domain.RequestsAggregate.ViewModels;
using Zindagi.Infra.Data;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Repositories
{
    public class BloodRequestRepository : IBloodRequestRepository
    {
        private readonly ZindagiDbContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IMapper _mapper;
        public IUnitOfWork UnitOfWork => _context;

        public BloodRequestRepository(ILogger<UserRepository> logger, ZindagiDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<BloodRequest> CreateAsync(BloodRequest request, CancellationToken ct = default)
        {
            var result = await _context.BloodRequests.AddAsync(request, ct);
            _logger.LogDebug("[User] [INSERT] [{result}]", result.State);
            return result.Entity;
        }

        public async Task<BloodRequest> GetAsync(long id, CancellationToken ct = default)
        {
            var result = await _context.BloodRequests.FindAsync(new object[] { id }, cancellationToken: ct);
            return result;
        }

        public async Task<bool> UpdateAsync(BloodRequest request, CancellationToken ct = default)
        {
            var result = _context.BloodRequests.Attach(request);
            result.State = EntityState.Modified;
            var save = await _context.SaveEntitiesAsync(ct);
            return save;
        }

        public async Task<BloodRequestDto> GetBloodRequest(long requestId, CancellationToken ct = default)
        {
            var result = await GetAsync(requestId, ct);
            return _mapper.Map<BloodRequestDto>(result);
        }

        public async Task<List<BloodRequestDto>> GetBloodRequests(long userId, CancellationToken ct = default)
        {
            var result = await _context.BloodRequests
                                            .Where(q => (q.Status == DetailedStatus.Open || q.Status == DetailedStatus.Assigned) && q.RequestorId == userId)
                                            .ToArrayAsync(ct);

            return _mapper.Map<List<BloodRequestDto>>(result);
        }

        public async Task<List<BloodRequestDto>> GetAcceptedBloodRequests(long userId, CancellationToken ct = default)
        {
            var result = await _context.BloodRequests
                                            .Where(q => q.Status == DetailedStatus.Assigned && q.AssigneeId == userId)
                                            .ToArrayAsync(ct);

            return _mapper.Map<List<BloodRequestDto>>(result);
        }

        public async Task<List<BloodRequestDto>> GetWaitingBloodRequests(CancellationToken ct = default)
        {
            var result = await _context.BloodRequests
                             .Where(q => (q.Status == DetailedStatus.None || q.Status == DetailedStatus.Open) && q.AssigneeId == null)
                             .ToArrayAsync(ct);

            return _mapper.Map<List<BloodRequestDto>>(result);
        }

        public async Task<List<BloodRequestDto>> GetWaitingBloodRequests(long userId, CancellationToken ct = default)
        {
            var result = await _context.BloodRequests
                                            .Where(q => (q.Status == DetailedStatus.None || q.Status == DetailedStatus.Open) && q.AssigneeId == null && q.RequestorId != userId)
                                            .ToArrayAsync(ct);

            return _mapper.Map<List<BloodRequestDto>>(result);
        }
    }
}
