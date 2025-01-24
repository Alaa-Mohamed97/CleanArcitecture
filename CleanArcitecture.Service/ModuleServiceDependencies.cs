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
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizeService, AuthorizeService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
            return services;
        }
    }
}
