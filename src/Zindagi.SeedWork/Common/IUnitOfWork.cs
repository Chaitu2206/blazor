using System;
using System.Threading;
using System.Threading.Tasks;

namespace Zindagi.SeedWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken ct = default);
        Task<bool> SaveEntitiesAsync(CancellationToken ct = default);
    }
}
