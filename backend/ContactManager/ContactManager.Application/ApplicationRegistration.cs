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
                cfg.AddProfile<EntityToDtoMapper>();
                cfg.AddProfile<RequestToEntityMapper>();
            });
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }
    }
}
