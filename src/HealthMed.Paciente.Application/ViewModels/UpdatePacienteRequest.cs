using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.ViewModels
{
    public record UpdatePacienteRequest( Guid Id, string Nome, string Cpf, string Email);
}
