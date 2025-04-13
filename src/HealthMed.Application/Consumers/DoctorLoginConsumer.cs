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

            var validationResult = await _doctorService.ValidateDoctorAsync(doctor.Crm, doctor.PasswordHash);

            await context.RespondAsync(new DoctorLoginEventResponse
            {
                IsAuthenticated = validationResult.IsAuthenticated,
                Name = validationResult.Name,
                ErrorMessage = validationResult.ErrorMessage
            });
        }
    }
}
