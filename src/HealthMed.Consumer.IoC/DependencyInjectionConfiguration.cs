using HealthMed.Application.Consumers;
using HealthMed.Application.Events;
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
            services.AddSingleton<IDoctorAgendaConsumerService, DoctorAgendaConsumerService>();
            services.AddSingleton<IDoctorAgendaGetConsumerService, DoctorAgendaGetConsumerService>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<DoctorInsertConsumer>();
                x.AddConsumer<DoctorLoginConsumer>();
                x.AddConsumer<DoctorAgendaInsertConsumer>();
                x.AddConsumer<DoctorAgendaGetConsumer>();

                x.AddRequestClient<DoctorLoginEvent>(new Uri("queue:doctor-login-queue"));
                x.AddRequestClient<DoctorAgendaInsertEvent>(new Uri("queue:doctor-agendainsert-queue"));
                x.AddRequestClient<DoctorAgendaGetEvent>(new Uri("queue:doctor-agendaget-queue"));

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

                    cfg.ReceiveEndpoint("doctor-agendainsert-queue", e =>
                    {
                        e.ConfigureConsumer<DoctorAgendaInsertConsumer>(context);
                    });

                    cfg.ReceiveEndpoint("doctor-agendaget-queue", e =>
                    {
                        e.ConfigureConsumer<DoctorAgendaGetConsumer>(context);
                    });


                    cfg.ConfigureEndpoints(context);
                });
            });

        }
    }
}
