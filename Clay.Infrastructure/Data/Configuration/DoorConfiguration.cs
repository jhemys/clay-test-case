using Clay.Domain.Aggregates.Door;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class DoorConfiguration : BaseConfiguration<Door>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Door> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .HasMany(p => p.AllowedRoles)
                .WithMany(p => p.Doors);

            builder
                .Navigation(p => p.AllowedRoles)
                .AutoInclude();
        }
    }
}
