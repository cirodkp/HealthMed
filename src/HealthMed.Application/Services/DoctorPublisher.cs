using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using MassTransit;

namespace HealthMed.Application.Services
{
    public class DoctorPublisher : IDoctorPublisher
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IRequestClient<DoctorLoginEvent> _loginRequestClient;

        public DoctorPublisher(ISendEndpointProvider sendEndpointProvider,
            IRequestClient<DoctorLoginEvent> loginRequestClient)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _loginRequestClient = loginRequestClient;
        }

        public async Task SendInsertDoctorAsync(InsertDoctorEvent doctorEvent)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:doctor-insert-queue"));
            await endpoint.Send(doctorEvent);
        }

        public async Task<bool> RequestLoginDoctorSync(DoctorLoginEvent doctorLoginEvent)
        {
            try
            {
                var response = await _loginRequestClient.GetResponse<DoctorLoginEventResponse>(doctorLoginEvent);
                return response.Message.IsAuthenticated;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
