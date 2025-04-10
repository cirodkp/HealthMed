using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorLoginConsumer : IConsumer<DoctorLogin>
    {
        private readonly IDoctorService _doctorService;

        public DoctorLoginConsumer(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public async Task Consume(ConsumeContext<DoctorLogin> context)
        {
            var doctor = new DoctorCredentials()
            {
                Crm = context.Message.Crm,
                Password = context.Message.Password
            };

            // Chama o serviço para validar o CRM e senha do médico
            var isValid = await _doctorService.LoginAsync(doctor);

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
