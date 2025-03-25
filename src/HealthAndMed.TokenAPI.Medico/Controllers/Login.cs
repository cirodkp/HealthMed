using HealthAndMed.Application.Commands;
using HealthAndMed.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthAndMed.TokenAPI.Medico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController(IAuthenticationUseCase authenticationService) : ControllerBase
    {
        private readonly IAuthenticationUseCase _authenticationService = authenticationService;

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
