using Microsoft.AspNetCore.Authorization;
using HealthMed.Application.Commands;
using HealthMed.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using HealthMed.Application.Models;
using HealthMed.Application.Results;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        /// <summary>
        /// Inclusão de um Médico 
        /// </summary>
        /// <param name="insertDoctorUseCase">Ação de inclusão do Médico</param>
        /// <param name="insertDoctorRequest">Dados do Médico para ser incluído</param>
        /// <returns>Retorna o Médico incluído</returns>
        /// <response code="200">Sucesso na inclusão do Médico</response>
        /// <response code="400">Não foi possível incluir o Médico</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add([FromServices] IInsertDoctorUseCase insertDoctorUseCase, InsertDoctorRequest insertDoctorRequest)
        {
            try
            {
                return Ok(await insertDoctorUseCase.Execute(insertDoctorRequest));

            }
            catch (Exception e) when (e is ApplicationException || e is ArgumentException)
            {
                return BadRequest(new ErrorMessageResponse(e.Message));
            }
        }
    }
}