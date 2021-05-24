using System.ComponentModel;

namespace Zindagi.Domain.RequestsAggregate
{
    public enum BloodRequestPriorityList
    {
        [Description("-SELECT-")]
        None = 0,

        [Description("Emergency (Immediate)")]
        Emergency = 1,

        [Description("High (Two Hours)")]
        High = 2,

        [Description("Medium (Six Hours)")]
        Medium = 3,

        [Description("Low (One Day or More)")]
        Low = 4
    }
}