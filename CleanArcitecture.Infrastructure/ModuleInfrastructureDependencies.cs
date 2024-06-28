using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArcitecture.Infrastructure
{
    public static class ModuleInfrastructureDependencies
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }
    }
}
