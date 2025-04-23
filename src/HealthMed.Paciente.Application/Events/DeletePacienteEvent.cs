using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.Events
{
  public  class DeletePacienteEvent
    {
        public Guid Id { get; set; }
    }
}
