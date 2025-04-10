using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.API.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAuthenticationService _authenticationService;


        public PatientController(IPatientService patientService, IAuthenticationService authenticationService)
        {
            _patientService = patientService;
            _authenticationService = authenticationService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] DoctorCredentials doctorCredentials)
        {
            var result = await _patientService.LoginAsync(doctorCredentials);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPost("GetAllDoctors")]
        public async Task<IActionResult> GetAllDoctors()
        {
            var result = await _patientService.GetAllDoctors();
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPost("GetDoctorsBySpeciality")]
        public async Task<IActionResult> GetDoctorsBySpeciality(DoctorSpecialtyEnum specialty)
        {
            var result = await _patientService.GetDoctorsBySpeciality(specialty);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }

        [HttpPost("RegisterAppoitment")]
        public async Task<IActionResult> RegisterAppoitment(Appointment appointment)
        {
            var result = await _patientService.RegisterAppoitment(appointment);
            if (result == null)
                return Unauthorized();
            return Ok(result);
        }
    }
}
