using Clay.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(e => e.Id);

            builder
                .Property(e => e.Name)
                .IsRequired();

            builder
                .Property(e => e.Email)
                .IsRequired();

            builder
                .Property(e => e.Role)
                .IsRequired();

            builder
                .Property(e => e.Password)
                .IsRequired();
        }
    }
}
