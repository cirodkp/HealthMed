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
  public  class UpdatePacientePublisher
    {
        private readonly IBus _bus;

        public UpdatePacientePublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishContactAsync(UpdatePacienteRequest request)
        {
            var updateEvent = new UpdatePacienteEvent
            {
                Id = request.Id,
                Nome = request.Nome,
                Email = request.Email 
            };

            await _bus.Publish(updateEvent);
        }
    }
}
