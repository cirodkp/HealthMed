using HealthMed.Consultation.Application.Interfaces;
using HealthMed.Consultation.Application.UseCases;
using HealthMed.Consultation.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Consultation.API.Controllers
{
    [Route("api/[controller]")]
    public class ConsultaController : ControllerBase
    {
        [HttpPost("agendar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Agendar([FromServices] IAgendarConsultaUseCase agendarConsultaUseCase, [FromBody] AgendarConsultaRequest request)
        {
            try
            {            
            await agendarConsultaUseCase.Execute(request);
            return Ok("Consulta agendada com sucesso.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao agendar consulta: " + ex.Message });
            }
        }

        [HttpPut("{id}/aceitar")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> Aceitar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase, int id)
        {
            try
            {

             
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Aceita", Justificativa = "" };
            await atualizarStatusUseCase.Execute(atualizarstatus);

            return Ok("Consulta aceita.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao aceitar consulta: " + ex.Message });
            }
        }

        [HttpPut("{id}/recusar")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> Recusar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase, int id, [FromBody] JustificativaRequest request)
        {
            try
            {

            
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Recusada", Justificativa = request.Justificativa };
            await atualizarStatusUseCase.Execute(atualizarstatus);
            return Ok("Consulta recusada.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao recusar consulta: " + ex.Message });
            }
        }

        [HttpPut("{id}/cancelar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Cancelar([FromServices] IAtualizarStatusUseCase atualizarStatusUseCase, int id, [FromBody] JustificativaRequest request)
        {
            try
            {

             
            var atualizarstatus = new AtualizarStatusRequest() { ConsultaId = id, NovoStatus = "Cancelada", Justificativa = request.Justificativa };
            await atualizarStatusUseCase.Execute(atualizarstatus);
            return Ok("Consulta cancelada.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao cancelar consulta do paciente: " + ex.Message });
            }
        }

        [HttpGet("medico")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> ConsultasDoMedico([FromServices] IGetConsultaUseCase getConsultaUseCase)
        {
            try
            {
 
            var crm = User.FindFirst("crm")?.Value;
            var result = await getConsultaUseCase.ListarPorCrmAsync(crm!);
            return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = $"Erro ao retornar consultas do  médico crm :{User.FindFirst("crm")?.Value} " + ex.Message });
            }
        }

        [HttpGet("paciente")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> ConsultasDoPaciente([FromServices] IGetConsultaUseCase getConsultaUseCase)
        {
            try
            {
                var cpf = User.FindFirst("cpf")?.Value;
                var result = await getConsultaUseCase.ListarPorCpfAsync(cpf!);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = "Erro ao retornar consultas do  paciente: " + ex.Message });
            }
        }

    }
}
