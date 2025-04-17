using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Domain.Entities
{
    public class DoctorAgendaGet
    {
        public string Crm { get; set; }
        public DateTime DataHoraAgenda { get; set; }
        public bool IsScheduled { get; set; }
    }
}
