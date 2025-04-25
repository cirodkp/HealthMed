using HealthMed.Consultation.Application.Interfaces;
using HealthMed.Consultation.Application.UseCases;
using HealthMed.Consultation.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Consultation.API.Controllers
{
    public class ConsultaController : ControllerBase
    {



        [HttpPost("agendar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Agendar([FromServices] IAgendarConsultaUseCase agendarConsultaUseCase , [FromBody] AgendarConsultaRequest request)
        {
            await agendarConsultaUseCase.Execute(request);
            return Ok("Consulta agendada com sucesso.");
        }

        [HttpPut("{id}/aceitar")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> Aceitar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase,int id)
        {
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Aceita", Justificativa = "" };
            await atualizarStatusUseCase.Execute(atualizarstatus);
         
            return Ok("Consulta aceita.");
        }

        [HttpPut("{id}/recusar")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> Recusar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase, int id, [FromBody] JustificativaRequest request)
        {
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Recusada", Justificativa = request.Justificativa };
            await atualizarStatusUseCase.Execute(atualizarstatus);
            return Ok("Consulta recusada.");
        }

        [HttpPut("{id}/cancelar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Cancelar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase, int id, [FromBody] JustificativaRequest request)
        {
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Cancelada", Justificativa = request.Justificativa };
            await atualizarStatusUseCase.Execute(atualizarstatus);
            return Ok("Consulta cancelada.");
        }

        [HttpGet("medico")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> ConsultasDoMedico([FromServices] IGetConsultaUseCase getConsultaUseCase)
        {
            var crm = User.FindFirst("crm")?.Value;
            var result = await getConsultaUseCase.ListarPorCrmAsync(crm!);
            return Ok(result);
        }

        [HttpGet("paciente")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> ConsultasDoPaciente([FromServices] IGetConsultaUseCase getConsultaUseCase)
        {
            var cpf = User.FindFirst("cpf")?.Value;
            var result = await getConsultaUseCase.ListarPorCpfAsync(cpf!);
            return Ok(result);
        }

    }
}
