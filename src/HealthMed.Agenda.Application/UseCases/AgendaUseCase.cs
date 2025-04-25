using HealthMed.Agenda.Application.Interfaces;
using HealthMed.Agenda.Application.ViewModels;
using HealthMed.Agenda.Domain.Entities;
using HealthMed.Agenda.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Application.UseCases
{
    public class AgendaUseCase(IAgendaRepository agendaRepository) : IAgendaUseCase
    {
        public async Task CadastrarHorarioAsync(HorarioDisponivelRequest request)
        {
            var horario = new HorarioDisponivel { MedicoId = request.MedicoId, DataHora = request.DataHora };
            await agendaRepository.AdicionarAsync(horario);
        }

        public async Task<List<HorarioDisponivelResponse>> ObterPorMedicoAsync(int medicoId)
        {
            var horarios = await agendaRepository.ObterPorMedicoAsync(medicoId);
            return horarios.Select(h => new HorarioDisponivelResponse(h.Id, h.MedicoId, h.DataHora, h.Ocupado)).ToList();
        }

        public async Task MarcarComoOcupadoAsync(int horarioId)
        {
            await agendaRepository.MarcarComoOcupadoAsync(horarioId);
        }
    }
}
