using HealthMed.Patient.Application.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.Interfaces
{
 public   interface IPacientePublisher
    {
        Task PublishInsertPacienteAsync(InsertPacienteEvent message);
        Task PublishUpdatePacienteAsync(UpdatePacienteEvent message);
        Task PublishDeletePacienteAsync(DeletePacienteEvent message);
    }
}
