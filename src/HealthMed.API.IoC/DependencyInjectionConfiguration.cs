using HealthMed.Application.Interfaces;
using HealthMed.Application.Services;
using HealthMed.Domain.Interfaces;
using HealthMed.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.API.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddSingleton<IAuthenticationService, AuthenticationServices>();
            services.AddScoped<IGenericRepository, GenericRepository>();

            const string serviceName = "API.Service";
        }
    }
}
