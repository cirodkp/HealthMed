using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using MassTransit;

namespace HealthMed.Application.Services
{
    public class DoctorPublisher : IDoctorPublisher
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public DoctorPublisher(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task SendInsertDoctorAsync(InsertDoctorEvent doctorEvent)
        {
            var endpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:doctor-insert-queue"));
            await endpoint.Send(doctorEvent);
        }
    }
}
