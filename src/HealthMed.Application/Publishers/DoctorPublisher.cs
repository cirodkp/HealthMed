using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using MassTransit;

namespace HealthMed.Application.Publishers
{
    public class DoctorPublisher : IDoctorPublisher
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRequestClient<DoctorLoginEvent> _loginRequestClient;
        private readonly IRequestClient<DoctorAgendaGetEvent> _agendaRequestClient;

        public DoctorPublisher(ISendEndpointProvider sendEndpointProvider, IRequestClient<DoctorLoginEvent> loginRequestClient, IRequestClient<DoctorAgendaGetEvent> agendaRequestClient)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _loginRequestClient = loginRequestClient;
            _agendaRequestClient = agendaRequestClient;
        }

        public async Task SendInsertDoctorAsync(InsertDoctorEvent doctorEvent)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:doctor-insert-queue"));
            await endpoint.Send(doctorEvent);
        }

        public async Task SendInsertDoctorAgendaAsync(DoctorAgendaInsertEvent doctorAgendaEvent)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:doctor-agendainsert-queue"));
            await endpoint.Send(doctorAgendaEvent);
        }

        public async Task<DoctorLoginEventResponse> RequestLoginDoctorSync(DoctorLoginEvent doctorLoginEvent)
        {
            try
            {
                var response = await _loginRequestClient.GetResponse<DoctorLoginEventResponse>(doctorLoginEvent);
                return response.Message;
            }
            catch (Exception ex)
            {
                return new DoctorLoginEventResponse
                {
                    IsAuthenticated = false,
                    ErrorMessage = "Erro na autenticação: " + ex.Message
                };
            }
        }

        public async Task<IEnumerable<DoctorAgendaGetEventResponse>> RequestDoctorAgendaGetSync(DoctorAgendaGetEvent doctorAgendaGetEvent)
        {
            try
            {

                var response = await _agendaRequestClient.GetResponse<DoctorAgendaGetListEventResponse>(doctorAgendaGetEvent);
                return response.Message.Items;

            }
            catch (Exception ex)
            {
                return new List<DoctorAgendaGetEventResponse>
                {   new DoctorAgendaGetEventResponse
                    {
                        Crm = doctorAgendaGetEvent.Crm,
                        ErrorMessage = "Erro na consulta: " + ex.Message,
                        IsScheduled = false,
                        DataHora = null
                    }
                };
            }
        }
    }
}
