using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorInsertConsumer : IConsumer<InsertDoctorEvent>
    {
        private readonly IDoctorConsumerService _doctorInsertService;

        public DoctorInsertConsumer(IDoctorConsumerService doctorInsertService)
        {
            _doctorInsertService = doctorInsertService;
        }

        public async Task Consume(ConsumeContext<InsertDoctorEvent> context)
        {
            var message = context.Message;

            var doctor = new DoctorInsert
            {
                Crm = message.Crm,
                Name = message.Name,
                Email = message.Email,
                PasswordHash = message.PasswordHash,
                KeyMFA = message.KeyMFA
            };

            await _doctorInsertService.InsertDoctorAsync(doctor);
        }
    }
}