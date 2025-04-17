using System.ComponentModel.DataAnnotations;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAgendaController : ControllerBase
    {

        /// <summary>
        /// Inclusão um horário na Agenda 
        /// </summary>
        /// <param name="doctorAgendaInsertService">Serviço de inclusão da Agenda do Médico</param>
        /// <param name="doctorAgendaInsertRequest">Dados da Agenda do Médico para ser incluído</param>
        /// <returns>Retorna a Agenda incluída</returns>
        /// <response code="202">Sucesso na inclusão da Agenda do Médico</response>
        /// <response code="400">Não foi possível incluir a Agenda do Médico</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Add([FromServices] IDoctorAgendaControllerInsertService doctorAgendaInsertService, DoctorAgendaControllerInsertRequest doctorAgendaInsertRequest)
        {
            try
            {
                try
                {
                    return Accepted(await doctorAgendaInsertService.Execute(doctorAgendaInsertRequest));

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

        /// <summary>
        /// Exclusão um horário na Agenda
        /// </summary>
        /// <param name="doctorAgendaDeleteService">Serviço de exclusão da Agenda do Médico</param>
        /// <param name="doctorAgendaDeleteRequest">Dados da Agenda do Médico para ser excluído</param>
        /// <returns>Retorna a Agenda excluída</returns>
        /// <response code="202">Sucesso na exclusão da Agenda do Médico</response>
        /// <response code="400">Não foi possível excluir a Agenda do Médico</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Delete([FromServices] IDoctorAgendaControllerDeleteService doctorAgendaDeleteService, DoctorAgendaControllerDeleteRequest doctorAgendaDeleteRequest)
        {
            try
            {
                try
                {
                    return Accepted(await doctorAgendaDeleteService.Execute(doctorAgendaDeleteRequest));

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

        /// <summary>
        /// Retorna as agendas do médico
        /// </summary>
        /// <param name="doctorAgendaGetService">Serviço de listagem da Agenda do Médico</param>
        /// <param name="crm">CRM do médico</param>
        /// <returns>Lista de horários agendados</returns>
        /// <response code="200">Sucesso na obtenção das Agendas</response>
        /// <response code="204">Não foi encontrado Agenda para os parâmetros informados</response>
        /// <response code="400">Erro de requisição</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Get([FromServices] IDoctorAgendaControllerGetService doctorAgendaGetService,[FromQuery][Required] string crm)
        {
            try
            {
                var request = new DoctorAgendaControllerGetRequest(crm);
                var agendas = await doctorAgendaGetService.Execute(request);

                if (!agendas.Any())
                    return NoContent();

                return Ok(agendas);
            }
            catch (KeyNotFoundException e)
            {
                return NotFound(new ErrorMessageResponse(e.Message));
            }
            catch (Exception e) when (e is ApplicationException || e is ArgumentException)
            {
                return BadRequest(new ErrorMessageResponse(e.Message));
            }
            catch (Exception e)
            {
                return StatusCode(500, new ErrorMessageResponse(e.Message));
            }
        }
    }
}