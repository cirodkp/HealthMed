using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using MassTransit;

namespace HealthMed.Application.Consumers
{
    public class DoctorAgendaGetConsumer : IConsumer<DoctorAgendaGetEvent>
    {
        private readonly IDoctorAgendaGetConsumerService _doctorAgendaGetService;

        public DoctorAgendaGetConsumer(IDoctorAgendaGetConsumerService doctorAgendaGetService)
        {
            _doctorAgendaGetService = doctorAgendaGetService;
        }

        public async Task Consume(ConsumeContext<DoctorAgendaGetEvent> context)
        {
            if (string.IsNullOrWhiteSpace(context.Message.Crm))
            {
                // Retorna lista vazia ou uma mensagem de erro customizada
                await context.RespondAsync(new DoctorAgendaGetListEventResponse
                {
                    Items = new List<DoctorAgendaGetEventResponse>()
                });
                return;
            }
            
            var crm = context.Message.Crm;

            var agendas = await _doctorAgendaGetService.GetAgendasByCrmAsync(crm);

            var response = new DoctorAgendaGetListEventResponse
            {
                Items = agendas.Select(a => new DoctorAgendaGetEventResponse
                {
                    Crm = a.Crm,
                    DataHora = a.DataHoraAgenda,
                    IsScheduled = a.IsScheduled
                }).ToList()
            };

            await context.RespondAsync(response);
        }
    }
}
