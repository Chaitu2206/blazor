using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Zindagi.Domain.UserAggregate;
using Zindagi.Infra.Data;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ILogger<UserRepository> _logger;
        private readonly ZindagiDbContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserRepository(ILogger<UserRepository> logger, ZindagiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<User> RegisterUserLoginAsync(User user, CancellationToken ct = default)
        {
            var userResult = await _context.Users.FirstOrDefaultAsync(q => q.AuthId == user.AuthId, ct) ?? await CreateAsync(user, ct);
            return userResult;
        }

        public async Task<User> CreateAsync(User user, CancellationToken ct = default)
        {
            var result = await _context.Users.AddAsync(user, ct);
            _logger.LogDebug("[User] [INSERT] [{result}]", result.State);
            return result.Entity;
        }

        public async Task<User> GetAsync(long id, CancellationToken ct = default)
        {
            var result = await _context.Users.FindAsync(new object[] { id }, ct);
            return result;
        }

        public async Task<User> GetAsync(VendorId authId, CancellationToken ct = default)
        {
            var result = await _context.Users.FirstOrDefaultAsync(q => q.AuthId == authId, ct);
            return result;
        }

        public async Task<User> UpdateAsync(User user, CancellationToken ct = default)
        {
            var result = _context.Users.Attach(user);
            result.State = EntityState.Modified;

            _logger.LogDebug("[User] [UPDATE] [{result}]", result.State);
            return await Task.FromResult(result.Entity);
        }

        public async Task<bool> DeleteAsync(long id, CancellationToken ct = default)
        {
            var result = _context.Users.Remove(await GetAsync(id, ct));

            _logger.LogDebug("[User] [DELETE] [{result}]", result.State);
            return await Task.FromResult(result.State == EntityState.Deleted);
        }

        public async Task<bool> DeleteAsync(VendorId authId, CancellationToken ct = default)
        {
            var result = _context.Users.Remove(await GetAsync(authId, ct));

            _logger.LogDebug("[User] [DELETE] [{result}]", result.State);
            return await Task.FromResult(result.State == EntityState.Deleted);
        }

        public async Task<bool> DeleteAsync(User user, CancellationToken ct = default)
        {
            var result = _context.Users.Remove(user);

            _logger.LogDebug("[User] [DELETE] [{result}]", result.State);
            return await Task.FromResult(result.State == EntityState.Deleted);
        }
    }
}
