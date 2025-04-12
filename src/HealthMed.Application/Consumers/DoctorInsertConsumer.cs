using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorInsertConsumer : IConsumer<DoctorInsert>
    {
        private readonly IDoctorConsumerService _doctorInsertService;

        public DoctorInsertConsumer(IDoctorConsumerService doctorInsertService)
        {
            _doctorInsertService = doctorInsertService;
        }

        public async Task Consume(ConsumeContext<DoctorInsert> context)
        {
            var doctor = context.Message;

            // Chama o serviço para validar o CRM e senha do médico
            var isValid = await _doctorInsertService.InsertDoctorAsync(doctor);

            if (isValid)
            {
                // Lógica Sucesso
            }
            else
            {
                // Lógica Falha
            }
        }
    }
}