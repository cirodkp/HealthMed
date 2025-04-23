using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Application.ViewModels
{
    public record HorarioDisponivelRequest(Guid MedicoId, DateTime DataHora);
}
