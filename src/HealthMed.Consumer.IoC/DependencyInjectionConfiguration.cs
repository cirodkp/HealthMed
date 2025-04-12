using HealthMed.Application.Consumers;
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

            services.AddSingleton<IDoctorDatabaseService>(sp =>
                new DoctorDatabaseService(configuration.GetConnectionString("PostgreSQL")));

            // Registrar o serviço de validação do médico
            services.AddSingleton<IDoctorConsumerService, DoctorConsumerService>();

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

                    cfg.ReceiveEndpoint("doctor-insert-queue", e =>
                    {
                        e.ConfigureConsumer<DoctorInsertConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("doctor-login-queue", e =>
                    {
                        e.ConfigureConsumer<DoctorLoginConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddConsumer<DoctorLoginConsumer>();
                x.AddConsumer<DoctorInsertConsumer>();

            });

        }
    }
}
