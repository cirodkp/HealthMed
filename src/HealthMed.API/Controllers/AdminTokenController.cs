using HealthMed.Application.Commands;
using HealthMed.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminTokenController(IAuthenticationAdminService authenticationService) : ControllerBase
    {
        private readonly IAuthenticationAdminService _authenticationService = authenticationService;

        /// <summary>
        /// Enviar credenciais para obter token de acesso (JwtBearer) 
        /// </summary>
        /// <param name="command">Dados do Usuário Admin no sistema</param>
        /// <returns>token</returns>
        /// <response code="200">Autenticação realizada com sucesso</response>
        /// <response code="401">Usuário inválido (não autorizado)</response>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AdminAuthenticationCommand command)
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
