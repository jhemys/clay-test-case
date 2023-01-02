using Clay.Domain.Aggregates.Door;
using Clay.Domain.Aggregates.DoorHistory;
using Clay.Domain.Aggregates.Employee;
using Clay.Domain.Aggregates.Login;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Clay.Infrastructure.Data
{
    public class ClayDbContext : DbContext
    {
        public ClayDbContext(DbContextOptions<ClayDbContext> options) : base(options) { }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Door> Doors { get; set; }
        public DbSet<DoorHistory> DoorHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
