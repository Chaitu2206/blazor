using System.Threading;
using System.Threading.Tasks;
using Zindagi.SeedWork;

namespace Zindagi.Domain.UserAggregate
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetAsync(long id, CancellationToken ct = default);
        Task<User> GetAsync(VendorId authId, CancellationToken ct = default);
        Task<User> RegisterUserLoginAsync(User user, CancellationToken ct = default);
        Task<User> CreateAsync(User user, CancellationToken ct = default);
        Task<User> UpdateAsync(User user, CancellationToken ct = default);
        Task<bool> DeleteAsync(long id, CancellationToken ct = default);
        Task<bool> DeleteAsync(VendorId authId, CancellationToken ct = default);
        Task<bool> DeleteAsync(User user, CancellationToken ct = default);
    }
}
