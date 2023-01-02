using Clay.Domain.Aggregates.Login;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class LoginConfiguration : BaseConfiguration<Login>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Login> builder)
        {
            builder
                .Property(p => p.Email)
                .IsRequired();

            builder
                .HasIndex(p => p.Email)
                .IsUnique();

            builder
                .Property(p => p.PermissionType)
                .IsRequired();

            builder
                .Property(p => p.Password)
                .IsRequired();

            builder
                .OwnsOne(p => p.Employee);
        }
    }
}
