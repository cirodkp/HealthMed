using HealthMed.Patient.Application.Interfaces;
using HealthMed.Patient.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Patient.API.Controllers
{
    [ApiController]
    [Route("api/pacientes")]
    public class PacienteController : ControllerBase
    {
        [HttpPost("Cadastrar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Cadastrar([FromServices] IInsertPacienteUseCase insertPacienteUseCase, [FromBody] InsertPacienteRequest request)
       => Ok(await insertPacienteUseCase.Execute(request));

        [HttpPut("Atualizar")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> Atualizar([FromServices] IUpdatePacienteUseCase updatePacienteUseCase ,  [FromBody] UpdatePacienteRequest request)
            => Ok(await updatePacienteUseCase.Execute( request));

        [HttpGet("ObterPorCpf")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> ObterPorCpf([FromServices] IGetPacienteUseCase getPacienteUseCase, string cpf)
            => Ok(await getPacienteUseCase.ObterPorCpfAsync(cpf));

        [HttpGet("ObterPorId")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> ObterPorId([FromServices] IGetPacienteUseCase getPacienteUseCase,Guid id)
            => Ok(await getPacienteUseCase.ObterPorIdAsync(id));

        [HttpGet("ObterTodos")]
        [Authorize(Roles = "paciente")]
        public async Task<IActionResult> ObterTodos([FromServices] IGetPacienteUseCase getPacienteUseCase)
            => Ok(await getPacienteUseCase.ObterTodosAsync());
    }
}
