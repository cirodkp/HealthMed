using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace HealthMed.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientAgendaController : ControllerBase
    {

        /// <summary>
        /// Reservar um horário na Agenda do Médico
        /// </summary>
        /// <param name="patientAgendaInsertService">Serviço de inclusão da Reserva da Agenda do Médico</param>
        /// <param name="patientAgendaInsertRequest">Dados da Reserva da Agenda do Médico para ser incluído</param>
        /// <returns>Retorna a Agenda incluída</returns>
        /// <response code="202">Sucesso na inclusão da Reserva da Agenda do Médico</response>
        /// <response code="400">Não foi possível incluir a Reserva da Agenda do Médico</response>
        /// <response code="401">Não autorizado</response>
        [HttpPost]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> Add([FromServices] IPatientAgendaControllerInsertService patientAgendaInsertService, PatientAgendaControllerInsertRequest patientAgendaInsertRequest)
        {
            try
            {
                try
                {
                    return Accepted(await patientAgendaInsertService.Execute(patientAgendaInsertRequest));

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
        /// Cancelamento de uma Reserva na Agenda
        /// </summary>
        /// <param name="patientAgendaDeleteService">Serviço de cancelamento da Reserva da Agenda</param>
        /// <param name="patientAgendaDeleteRequest">Dados da Reserva da Agenda para ser cancelada</param>
        /// <returns>Retorna a Reserva da Agenda cancelada</returns>
        /// <response code="202">Sucesso no cancelamento da Reserva da Agenda</response>
        /// <response code="400">Não foi possível excluir a reserva da Agenda</response>
        /// <response code="401">Não autorizado</response>
        [HttpDelete]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> Delete([FromServices] IPatientAgendaControllerDeleteService patientAgendaDeleteService, PatientAgendaControllerDeleteRequest patientAgendaDeleteRequest)
        {
            try
            {
                try
                {
                    return Accepted(await patientAgendaDeleteService.Execute(patientAgendaDeleteRequest));

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
        /// Retorna as agendas do Paciente
        /// </summary>
        /// <param name="patientAgendaGetService">Serviço de listagem da Agenda do Paciente</param>
        /// <param name="cpf">CPF do Paciente</param>
        /// <returns>Lista de horários agendados</returns>
        /// <response code="200">Sucesso na obtenção das Agendas</response>
        /// <response code="400">Erro de requisição</response>
        /// <response code="401">Não autorizado</response>
        [HttpGet]
        [Authorize(Roles = "Admin,Patient")]
        public async Task<IActionResult> Get([FromServices] IPatientAgendaControllerGetService patientAgendaGetService, [FromQuery][Required] string cpf)
        {
            try
            {
                var request = new PatientAgendaControllerGetRequest(cpf);
                var agendas = await patientAgendaGetService.Execute(request);

                return Ok(agendas);
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
