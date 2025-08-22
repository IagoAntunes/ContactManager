using ContactManager.Application.Mapping;
using ContactManager.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager.Application
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection AddApplicationServices(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<RequestToDtoMapper>();
            });
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
