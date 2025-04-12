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
         /// <param name="insertDoctorService">Serviço de inclusão do Médico</param>
         /// <param name="insertDoctorRequest">Dados do Médico para ser incluído</param>
         /// <returns>Retorna o Médico incluído</returns>
         /// <response code="202">Sucesso na inclusão do Médico</response>
         /// <response code="400">Não foi possível incluir o Médico</response>
         /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add([FromServices] IInsertDoctorService insertDoctorService, InsertDoctorRequest insertDoctorRequest)
        {
            try
            {
                try
                {
                    return Accepted(await insertDoctorService.Execute(insertDoctorRequest));

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