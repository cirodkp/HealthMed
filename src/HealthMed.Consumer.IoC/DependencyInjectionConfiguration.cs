using HealthMed.Application.Interfaces;
using HealthMed.Application.Services;
using HealthMed.Infra;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Consumer.IoC
{
    public static class DependencyInjectionConfiguration
    {
        public static void AddInjections(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IDatabaseService>(sp =>
                new DatabaseService(configuration.GetConnectionString("PostgreSQL")));

            // Registrar o serviço de validação do médico
            services.AddSingleton<IDoctorService, DoctorService>();

            // Registrar o consumidor do RabbitMQ
            services.AddSingleton<DoctorConsumerService>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {

                    var rabbitMqHost = configuration["RabbitMQ:RABBITMQ_HOST"];
                    var rabbitMqUser = configuration["RabbitMQ:RABBITMQ_USER"];
                    var rabbitMqPassword = configuration["RabbitMQ:RABBITMQ_PASSWORD"];

                    cfg.Host(rabbitMqHost, h =>
                    {
                        h.Username(rabbitMqUser);
                        h.Password(rabbitMqPassword);
                    });

                    cfg.ReceiveEndpoint("doctor-login-queue", e =>
                    {
                        e.ConfigureConsumer<DoctorConsumerService>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddConsumer<DoctorConsumerService>();

            });

        }
    }
}
