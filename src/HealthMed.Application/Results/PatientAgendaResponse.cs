using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Results
{
    public record PatientAgendaResponse(
         string Cpf,
         DateTime DataHora,
         bool IsScheduled
    );
}
