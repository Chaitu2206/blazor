using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Data.Converters
{
    public class VendorIdConverter : ValueConverter<VendorId, string>
    {
        public VendorIdConverter(ConverterMappingHints? mappingHints = null) :
            base(obj => obj.ToString(),
                 val => VendorId.Create(val), mappingHints)
        { }
    }
}
