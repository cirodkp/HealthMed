using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Services
{
    public class DoctorConsumerService : IConsumer<DoctorLogin>
    {
        private readonly IDoctorService _doctorService;

        public DoctorConsumerService(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<DoctorLogin> context)
        {
            var doctor = context.Message;

            // Chama o serviço para validar o CRM e senha do médico
            var isValid = await _doctorService.ValidateDoctorAsync(doctor.Crm, doctor.Password);

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
