using Clay.Domain.Interfaces.Repositories;
using Clay.Infrastructure.Data;
using Clay.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Clay.Infrastructure.ServicesExtension
{
    public static class ServicesExtension
    {
        public static void AddDatabaseConnection(this IServiceCollection services)
        {
            var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTIONSTRING") ?? "name=ConnectionStrings:ClayDbContext";
            services.AddDbContext<ClayDbContext>(options => options.UseSqlServer(connectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(ServicesExtension).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
        }

        public static void MigrateDatabase(this IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var dataContext = scope.ServiceProvider.GetRequiredService<ClayDbContext>();
                dataContext.Database.Migrate();
            }
        }
    }
}
