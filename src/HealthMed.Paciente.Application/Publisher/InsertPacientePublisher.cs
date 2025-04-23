using HealthMed.Patient.Application.Events;
using HealthMed.Patient.Application.ViewModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.Publisher
{
   public class InsertPacientePublisher
    {
        private readonly IBus _bus;

        public InsertPacientePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishContactAsync(InsertPacienteRequest request)
        {
            var contactEvent = new InsertPacienteEvent
            {
                Nome = request.Nome,
                Email = request.Email,
                Cpf = request.Cpf
            };

            await _bus.Publish(contactEvent);
        }
    }
}
