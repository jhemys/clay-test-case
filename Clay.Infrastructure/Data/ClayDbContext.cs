using Clay.Domain.Aggregates.Employee;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clay.Infrastructure.Data
{
    public class ClayDbContext : DbContext
    {
        public ClayDbContext(DbContextOptions<ClayDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
