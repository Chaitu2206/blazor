using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zindagi.Domain.UserAggregate;
using Zindagi.Infra.Data.Converters;

namespace Zindagi.Infra.Data.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasColumnName("user_id")
                .HasValueGenerator<LongIdGenerator>()
                .UseHiLo("global")
                .ValueGeneratedNever();

            builder.HasAlternateKey(p => p.AuthId);
            builder.Property(p => p.AuthId)
                .HasConversion(CommonConverters.VendorIdValueConverter)
                .HasMaxLength(200);

            builder.HasIndex(p => p.Email).IsUnique();

            builder.Property(p => p.Email)
                .HasMaxLength(500);

            builder.Property(p => p.FirstName)
                .HasMaxLength(100);

            builder.Property(p => p.MiddleName)
                .HasMaxLength(100);

            builder.Property(p => p.LastName)
                .HasMaxLength(100);

            builder.Property(p => p.BloodGroup)
                .HasConversion(CommonConverters.BloodGroupValueConverter);

            builder.Property(p => p.IsActive)
                .HasConversion(CommonConverters.StatusValueConverter);

            builder.Property(p => p.IsDonor)
                .HasConversion(CommonConverters.StatusValueConverter);

            builder.Property(p => p.IsEmailVerified)
                .HasConversion(CommonConverters.StatusValueConverter);

            builder.HasMany(p => p.CreatedBloodRequests)
                .WithOne(p => p.Requestor)
                .HasForeignKey(p => p.RequestorId);

            builder.HasMany(p => p.AssignedBloodRequests)
                .WithOne(p => p.Assignee!)
                .HasForeignKey(p => p.AssigneeId);
        }
    }
}
