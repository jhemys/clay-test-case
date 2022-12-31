using Clay.Domain.Aggregates.Door;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class RoleConfiguration : BaseConfiguration<Role>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Role> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .HasIndex(p => p.Name)
                .IsUnique();
        }
    }
}
