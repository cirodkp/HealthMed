using HealthMed.Application.Commands;
using HealthMed.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController(IAuthenticationService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        /// <summary>
        /// Enviar credenciais para obter token de acesso (JwtBearer) 
        /// </summary>
        /// <param name="command">Dados do médico cadastrado no sistema</param>
        /// <returns>token</returns>
        /// <response code="200">Autenticação realizada com sucesso</response>
        /// <response code="401">CRM/Senha inválida (não autorizado)</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DoctorAuthenticationCommand command)
        {
            try
            {
                return Ok(await _authenticationService.Execute(command));
            }
            catch(Exception ex) when (ex is ArgumentException)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
