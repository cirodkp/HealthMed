using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorAgendaInsertConsumer : IConsumer<DoctorAgendaInsertEvent>
    {
        private readonly IDoctorAgendaConsumerService _doctorInsertService;

        public DoctorAgendaInsertConsumer(IDoctorAgendaConsumerService doctorAgendaInsertService)
        {
            _doctorInsertService = doctorAgendaInsertService;
        }

        public async Task Consume(ConsumeContext<DoctorAgendaInsertEvent> context)
        {
            var message = context.Message;

            var doctorAgenda = new DoctorAgendaInsert
            {
                Crm = message.Crm,
                DataHoraAgenda = message.DataHora
            };

            await _doctorInsertService.DoctorAgendaInsertAsync(doctorAgenda);
        }
    }
}
