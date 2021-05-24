using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using Zindagi.Infra.Data;
using Zindagi.SeedWork;

namespace Zindagi
{
    public static class InfraExtensions
    {
        public static Assembly Assembly() => typeof(InfraExtensions).Assembly;

        public static async Task DispatchDomainEventsAsync(this IMediator mediator, ZindagiDbContext ctx)
        {
            var domainEntities = ctx.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents?.Count > 0 ? x.Entity.DomainEvents : Array.Empty<INotification>())
                .ToList();

            domainEntities.ToList().ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }

        public static bool IsDevelopment(this IConfiguration config) => string.Equals(config["Environment"]?.ToUpperInvariant(), "DEVELOPMENT", StringComparison.Ordinal);
        public static bool IsStaging(this IConfiguration config) => string.Equals(config["Environment"]?.ToUpperInvariant(), "STAGING", StringComparison.Ordinal);
        public static bool IsProduction(this IConfiguration config) => string.Equals(config["Environment"]?.ToUpperInvariant(), "PRODUCTION", StringComparison.Ordinal);
    }
}
