using HealthMed.Patient.Domain.Core;
using HealthMed.Patient.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Domain.Interfaces
{
  public  interface IPacienteRepository : IRepository
    {
        Task<Paciente?> ObterPorCpfAsync(string cpf);
        Task<Paciente?> ObterPorIdAsync(Guid id);
        Task<List<Paciente>> ObterTodosAsync();
        Task AdicionarAsync(Paciente paciente);
        Task AtualizarAsync(Paciente paciente);
        Task RemoverAsync(Paciente paciente);
    }
}
