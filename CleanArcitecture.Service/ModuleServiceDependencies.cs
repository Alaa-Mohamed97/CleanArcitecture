using CleanArcitecture.Service.Abstracts;
using CleanArcitecture.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArcitecture.Service
{
    public static class ModuleServiceDependencies
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            return services;
        }
    }
}
