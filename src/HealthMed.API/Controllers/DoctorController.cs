using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly IAuthenticationService _authenticationService;


        public DoctorController(IDoctorService doctorService, IAuthenticationService authenticationService)
        {
            _doctorService = doctorService;
            _authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] DoctorCredentials doctorCredentials)
        {
            var result = await _doctorService.LoginAsync(doctorCredentials);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }


        [HttpGet("schedule/{id}")]
        public async Task<IActionResult> GetScheduleAsync(DateTime start, DateTime end)
        {
            var schedules = await _doctorService.GetAllSheduleAsync(start, end);
            return Ok(schedules);
        }

        [HttpPost("Insert")]
        public async Task<IActionResult> InsertScheduleAsync([FromBody] DoctorSchedule schedule)
        {
            var result = await _doctorService.RegisterScheduleAsync(schedule);
            return Ok();
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditScheduleAsync(Guid id, [FromBody] DoctorSchedule schedule)
        {
            var result = await _doctorService.UpdateSheduleAsync(id, schedule);
            return Ok(result);
        }


        [HttpDelete("accept/appointment/{id}")]
        public async Task<IActionResult> AcceptAppointmentAsync(Guid id, AppointmentStatus status)
        {
            var result = await _doctorService.AcceptAppointment(id, status, null);
            return Ok(result);
        }


        [HttpDelete("cancel/appointment/{id}")]
        public async Task<IActionResult> CancelAppointmentAsync(Guid id, AppointmentStatus status,[FromBody] string reason)
        {
            var result = await _doctorService.CancelAppointment(id, status, reason);
            return Ok(result);
        }
    }
}
