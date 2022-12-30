using Clay.Application.Interfaces.Repositories;
using Clay.Infrastructure.Data;
using Clay.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text;

namespace Clay.Infrastructure.ServicesExtension
{
    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static void AddDatabaseConnection(this IServiceCollection services, string connectionString)
        {
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

        public static void AddJwtAuthentication(this IServiceCollection services, string jwtSecretKey)
        {
            var key = Encoding.ASCII.GetBytes(jwtSecretKey);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
