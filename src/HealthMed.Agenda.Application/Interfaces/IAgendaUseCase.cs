using HealthMed.Agenda.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Application.Interfaces
{
  public  interface IAgendaUseCase
    {
        Task CadastrarHorarioAsync(HorarioDisponivelRequest request);
        Task<List<HorarioDisponivelResponse>> ObterPorMedicoAsync(Guid medicoId);
        Task MarcarComoOcupadoAsync(Guid horarioId);
    }
}
