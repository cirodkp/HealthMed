using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Application.Events;
using HealthMed.Application.Models;
using MassTransit;

namespace HealthMed.Application.Publishers
{
    public class InsertDoctorPublisher
    {
        private readonly IBus _bus;

        public InsertDoctorPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishDoctorAsync(InsertDoctorRequest request)
        {
            var insertEvent = new InsertDoctorEvent
            {
                Crm = request.Crm,
                Name = request.Name,
                Email = request.Email,
                PasswordHash = request.PasswordHash,
                KeyMFA = request.KeyMFA
            };

            await _bus.Publish(insertEvent);
        }
    }
}
