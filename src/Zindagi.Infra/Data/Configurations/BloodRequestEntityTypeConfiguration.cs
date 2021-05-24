using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zindagi.Domain.RequestsAggregate;
using Zindagi.Infra.Data.Converters;
using Zindagi.SeedWork;

namespace Zindagi.Infra.Data.Configurations
{
    public class BloodRequestEntityTypeConfiguration : IEntityTypeConfiguration<BloodRequest>
    {
        public void Configure(EntityTypeBuilder<BloodRequest> builder)
        {

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("blood_request_id")
                .HasValueGenerator<LongIdGenerator>()
                .ValueGeneratedNever();

            builder.Property(p => p.PatientName)
                .HasMaxLength(200);

            builder.Property(p => p.Reason)
                .HasMaxLength(500);

            builder.Ignore(p => p.QuantityInMl);

            builder.Property(p => p.DonationType)
                .HasConversion(p => p.Id, p => Enumeration.FromValue<BloodDonationType>(p));

            builder.Property(p => p.BloodGroup)
                .HasConversion(CommonConverters.BloodGroupValueConverter);

            builder.Property(p => p.Priority)
                .HasConversion(p => p.Id, p => Enumeration.FromValue<BloodRequestPriority>(p));

            builder.Property(p => p.Status)
                .HasConversion(CommonConverters.DetailedStatusValueConverter);

        }
    }
}
