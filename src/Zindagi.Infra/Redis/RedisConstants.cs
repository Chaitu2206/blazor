using StackExchange.Redis;
using Zindagi.Domain.RequestsAggregate;

namespace Zindagi.Infra.Redis
{
    public static class RedisConstants
    {
        public static readonly string BloodRequestsSearchSchema = nameof(BloodRequest).ToUpperInvariant();
        public static readonly RedisChannel NewBloodRequestChannel = new($"URN:{BloodRequestsSearchSchema}:NEW", RedisChannel.PatternMode.Auto);
    }
}
