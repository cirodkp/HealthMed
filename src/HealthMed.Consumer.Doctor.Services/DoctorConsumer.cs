using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Consumer.Doctor.Models;
using MassTransit;

namespace HealthMed.Consumer.Doctor.Services
{
    public class DoctorConsumer : IConsumer<DoctorLogin>
    {
        private readonly IDoctorService _doctorService;

        public DoctorConsumer(IDoctorService doctorService)
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
