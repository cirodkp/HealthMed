using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminPatientController : ControllerBase
    {

        /// <summary>
        /// Inclusão de um Paciente
        /// </summary>
        /// <param name="patientAPIInsertService">Serviço de inclusão do Paciente</param>
        /// <param name="patientAPIInsertRequest">Dados do Paciente para ser incluído</param>
        /// <returns>Retorna o Médico incluído</returns>
        /// <response code="202">Sucesso na inclusão do Paciente</response>
        /// <response code="400">Não foi possível incluir o Paciente</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromServices] IPatientControllerInsertService patientAPIInsertService, PatientControllerInsertRequest patientAPIInsertRequest)
        {
            try
            {
                try
                {
                    return Accepted(await patientAPIInsertService.Execute(patientAPIInsertRequest));

                }
                catch (Exception e) when (e is ApplicationException || e is ArgumentException)
                {
                    return BadRequest(new ErrorMessageResponse(e.Message));
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorMessageResponse(e.Message));
            }
        }
    }
}
