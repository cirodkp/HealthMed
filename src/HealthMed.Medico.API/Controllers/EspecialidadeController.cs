using HealthMed.Doctor.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Doctor.API.Controllers
{
    public class EspecialidadeController :   ControllerBase
    {
        /// <summary>
        /// Retorna todos os Especialidades incluídas
        /// </summary>
        /// <param name="GetEspecialidadeUseCase"></param>
        /// <returns>Retorna a </returns>
        /// <response code="200">Sucesso na execução do retorno dos especialidades</response>
        /// <response code="400">Não foi possível retornar os especialidades</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("ListarTodas")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> ListarTodasAsync([FromServices] IGetEspecialidadeUseCase getEspecUseCase)
        {
            return Ok(await getEspecUseCase.ListarTodasAsync());
        }

        /// <summary>
        /// Retorna as especialidades por Nome
        /// </summary>
        /// <param name="GetEspecialidadeUseCase">Retorna as especialidades incluídas</param>
        /// <param name="Nome">Informe o nome da especialidade</param>
        /// <returns>Retorna a lista de especialidades incluídas</returns>
        /// <response code="200">Sucesso na execução do retorno das especialidades</response>
        /// <response code="400">Não foi possível retornar os especialidades</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("GetByNome")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> GetByNome([FromServices] IGetEspecialidadeUseCase getEspecUseCase, [FromQuery] string Nome)
        {
            return Ok(await getEspecUseCase.GetByNome(Nome));
        }

        /// <summary>
        /// Retorna as especialidades por categoria
        /// </summary>
        /// <param name="GetEspecialidadeUseCase">Retorna as especialidades incluídas</param>
        /// <param name="Nome">Informe a Categoria da especialidade</param>
        /// <returns>Retorna a lista de categorias incluídas</returns>
        /// <response code="200">Sucesso na execução do retorno dos especialidades</response>
        /// <response code="400">Não foi possível retornar os especialidades</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet("GetByCategoria")]
        [Authorize(Roles = "medico")]
        public async Task<IActionResult> GetByCategoria([FromServices] IGetEspecialidadeUseCase getEspecUseCase, [FromQuery] string Categoria)
        {
            return Ok(await getEspecUseCase.GetByCategoria(Categoria));
        }
    }
}
