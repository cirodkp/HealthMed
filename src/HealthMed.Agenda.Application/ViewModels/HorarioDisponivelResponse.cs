using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Application.ViewModels
{
    public record HorarioDisponivelResponse(Guid Id, Guid MedicoId, DateTime DataHora, bool Ocupado);
}
