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
                .OwnsOne(p => p.Role);

            builder
                .OwnsOne(p => p.Role, a =>
                {
                    a.Property(p => p.Name ).HasColumnName("Role").IsRequired();
                });

            builder
                .Property(e => e.Password)
                .IsRequired();
        }
    }
}
