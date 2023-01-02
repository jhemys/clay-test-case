using Clay.Domain.Aggregates.DoorHistory;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class DoorHistoryConfiguration : BaseConfiguration<DoorHistory>
    {
        public override void ConfigureProperties(EntityTypeBuilder<DoorHistory> builder)
        {
            builder
                .Property(p => p.EmployeeName)
                .IsRequired();

            builder
                .Property(p => p.CurrentState)
                .IsRequired();

            builder
                .Ignore(p => p.IsRemoteAttempt);

            builder
                .Ignore(p => p.IsActive);
        }
    }
}
