using HealthMed.Consumer.Doctor.Infra;
using HealthMed.Consumer.Doctor.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HealthMed.Consumer.Doctor.IoC
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
            services.AddSingleton<DoctorConsumer>();

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
                        e.ConfigureConsumer<DoctorConsumer>(context);
                    });

                    cfg.ConfigureEndpoints(context);
                });

                x.AddConsumer<DoctorConsumer>();

            });

        }
    }
}
