using Clay.Infrastructure.Data;
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
    }
}
