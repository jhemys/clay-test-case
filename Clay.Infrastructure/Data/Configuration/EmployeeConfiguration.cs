﻿using Clay.Domain.Aggregates.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clay.Infrastructure.Data.Configuration
{
    public class EmployeeConfiguration : BaseConfiguration<Employee>
    {
        public override void ConfigureProperties(EntityTypeBuilder<Employee> builder)
        {
            builder
                .Property(p => p.Name)
                .IsRequired();

            builder
                .Property(p => p.Email)
                .IsRequired();

            builder
                .Property(p => p.Role)
                .IsRequired();

            builder
                .Property(p => p.Password)
                .IsRequired();
        }
    }
}
