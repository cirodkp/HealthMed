using HealthMed.Application.Interfaces;
using HealthMed.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.API.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddSingleton<IAuthenticationAdminService, AuthenticationAdminServices>();
            services.AddSingleton<IAuthenticationDoctorService, AuthenticationDoctorServices>();

            const string serviceName = "API.Service";
        }
    }
}
