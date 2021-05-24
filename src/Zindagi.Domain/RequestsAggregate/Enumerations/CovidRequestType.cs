using System.Collections.Generic;

namespace Zindagi.Domain.RequestsAggregate
{
    public record CovidRequestType(int Id, string Description)
    {
        public static CovidRequestType Oximeter { get; } = new(1, "Pulse Oximeter");
        public static CovidRequestType BedWithOxygen { get; } = new(1, "Bed (Oxygen)");

        public static readonly IReadOnlyList<CovidRequestType> All = new[] {
            Oximeter,
            BedWithOxygen
        };
    }
}