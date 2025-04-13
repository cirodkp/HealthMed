using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Events
{
    public class DoctorLoginEvent
    {
        public required string Crm { get; set; }
        public required string PasswordHash { get; set; }
    }

    public class DoctorLoginEventResponse
    {
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
