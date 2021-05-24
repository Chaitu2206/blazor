using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Zindagi.Domain;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Domain.UserAggregate;
using Zindagi.Infra.Data.Configurations;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Data
{
    public class ZindagiDbContext : DbContext, IUnitOfWork
    {
        public const string SCHEMA = "zg";
        public const string MIGRATIONS = "ef_migrations";
        public DbSet<BloodRequest> BloodRequests { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;

        private readonly IMediator _mediator;
        private readonly ILogger<ZindagiDbContext> _logger;
        private IDbContextTransaction? _currentTransaction;
        private static readonly Type[] EnumerationTypes =
        {
            typeof(BloodDonationType),
            typeof(BloodGroup),
            typeof(BloodRequestPriority),
            typeof(DetailedStatus)
        };

        public bool HasActiveTransaction => _currentTransaction != null;

        public IDbContextTransaction? GetCurrentTransaction() => _currentTransaction;

        //// public ZindagiDbContext(DbContextOptions<ZindagiDbContext> options) : base(options) { }

        public ZindagiDbContext(DbContextOptions<ZindagiDbContext> options, IMediator mediator, ILogger<ZindagiDbContext> logger) : base(options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger;

            _logger.LogTrace("ZindagiDbContext::ctor ->" + base.GetHashCode());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(SCHEMA);
            modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new BloodRequestEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken ct = default)
        {
            var enumerationEntities = ChangeTracker.Entries().Where(p => EnumerationTypes.Contains(p.Entity.GetType()));

            foreach (var enumerationEntity in enumerationEntities)
                enumerationEntity.State = EntityState.Unchanged;

            await _mediator.DispatchDomainEventsAsync(this);

            var result = await base.SaveChangesAsync(ct);
            _logger.LogDebug("[SaveEntities] EF Save Entities Result [{result}]", result);
            return true;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            _logger.LogDebug("[SaveChanges] EF Save Changes");
            return base.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            if (_currentTransaction != null)
                return null!;

            _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

            return _currentTransaction;
        }

        public async Task CommitTransactionAsync(IDbContextTransaction transaction)
        {
            Fail.IfNull(transaction);
            if (transaction != _currentTransaction)
                throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}
