using HealthMed.Consultation.Application.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Application.Publisher
{
  public  class AtualizarStatusPublisher
    {
        private readonly IBus _bus;

        public AtualizarStatusPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishContactAsync(AtualizarStatusEvent request)
        {
            var updateEvent = new AtualizarStatusEvent
            {
                Id = request.Id,
                Status = request.Status,
                Justificativa = request.Justificativa 
            };

            await _bus.Publish(updateEvent);
        }
    }
}
