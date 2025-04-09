using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using MassTransit;

namespace HealthMed.Application.Services
{
    public class DoctorPublisher : IDoctorPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public DoctorPublisher(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishInsertDoctorAsync(InsertDoctorEvent doctorEvent)
        {
            await _publishEndpoint.Publish(doctorEvent);
        }
    }
}
