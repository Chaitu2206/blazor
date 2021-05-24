using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Zindagi.Domain;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Data.Converters
{
    public class DetailedStatusConverter : ValueConverter<DetailedStatus, int>
    {
        public DetailedStatusConverter(ConverterMappingHints? mappingHints = null) :
            base(obj => obj.Id,
                 val => Enumeration.FromValue<DetailedStatus>(val), mappingHints)
        { }
    }
}