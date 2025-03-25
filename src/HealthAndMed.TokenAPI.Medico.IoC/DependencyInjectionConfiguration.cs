using HealthAndMed.Application.Interfaces;
using HealthAndMed.Application.UseCases;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthAndMed.TokenAPI.Medico.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddInjections(this IServiceCollection services, IConfiguration configuration)
        {
            //Services
            services.AddSingleton<IAuthenticationUseCase, AuthenticationUseCase>();

            const string serviceName = "TokenAPI.Medico.Service";
        }
    }
}
