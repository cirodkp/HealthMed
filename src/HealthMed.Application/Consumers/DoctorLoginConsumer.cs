using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorLoginConsumer : IConsumer<DoctorLoginEvent>
    {
        private readonly IDoctorConsumerService _doctorService;

        public DoctorLoginConsumer(IDoctorConsumerService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<DoctorLoginEvent> context)
        {
            var doctor = context.Message;

            // Chama o serviço para validar o CRM e senha do médico
            var isValid = await _doctorService.ValidateDoctorAsync(doctor.Crm, doctor.PasswordHash);

            if (isValid)
            {
                await context.RespondAsync(new DoctorLoginEventResponse
                {
                    IsAuthenticated = true,
                    ErrorMessage = null
                });
            }
            else
            {
                await context.RespondAsync(new DoctorLoginEventResponse
                {
                    IsAuthenticated = false,
                    ErrorMessage = "CRM ou senha inválidos"
                });
            }
        }
    }
}
