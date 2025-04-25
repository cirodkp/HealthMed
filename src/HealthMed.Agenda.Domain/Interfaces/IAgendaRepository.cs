using HealthMed.Agenda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Domain.Interfaces
{
   public interface IAgendaRepository
    {
        Task<List<HorarioDisponivel>> ObterPorMedicoAsync(int medicoId);
        Task AdicionarAsync(HorarioDisponivel horario);
        Task AtualizarAsync(HorarioDisponivel horario);
        Task MarcarComoOcupadoAsync(int id);
    }
}
