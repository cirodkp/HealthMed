using HealthMed.Agenda.Application.Interfaces;
using HealthMed.Agenda.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Agenda.API.Controllers
{
    [ApiController]
    [Route("api/agenda")]
    public class AgendaController : ControllerBase
    {
        private readonly IAgendaUseCase _useCase;

        public AgendaController(IAgendaUseCase useCase)
        {
            _useCase = useCase;
        }

        [HttpPost("horarios")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> Cadastrar([FromBody] HorarioDisponivelRequest request)
        {
            await _useCase.CadastrarHorarioAsync(request);
            return Ok("Horário cadastrado com sucesso.");
        }

        [HttpGet("medico/{medicoId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPorMedico(int medicoId)
        {
            var result = await _useCase.ObterPorMedicoAsync(medicoId);
            return Ok(result);
        }

        [HttpPatch("ocupar/{horarioId}")]
        [Authorize(Roles = "sistema")]
        public async Task<IActionResult> MarcarComoOcupado(int horarioId)
        {
            await _useCase.MarcarComoOcupadoAsync(horarioId);
            return Ok("Horário marcado como ocupado.");
        }
    }
}
 
