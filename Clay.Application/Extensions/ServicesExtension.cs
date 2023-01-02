using Clay.Application.Interfaces.Services;
using Clay.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Clay.Application.Extensions
{

    [ExcludeFromCodeCoverage]
    public static class ServicesExtension
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddTransient<IDoorService, DoorService>();
            services.AddTransient<IDoorHistoryService, DoorHistoryService>();
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
