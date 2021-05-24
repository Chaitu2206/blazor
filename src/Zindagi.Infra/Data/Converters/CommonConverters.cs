using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zindagi.Domain;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Data.Converters
{
    public static class CommonConverters
    {
        public static readonly ValueConverter<Status, int> StatusValueConverter
            = new(o => o.Id,
                v => Enumeration.FromValue<Status>(v));

        public static readonly ValueConverter<BloodGroup, int> BloodGroupValueConverter
            = new(o => o.Id,
                v => Enumeration.FromValue<BloodGroup>(v));

        public static readonly ValueConverter<DetailedStatus, int> DetailedStatusValueConverter
            = new(o => o.Id,
                v => Enumeration.FromValue<DetailedStatus>(v));

        public static readonly ValueConverter<VendorId, string> VendorIdValueConverter
            = new(o => o.Value,
                v => VendorId.Create(v));
    }
}
