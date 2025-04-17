using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Results;
using HealthMed.Infra;

namespace HealthMed.Application.Services
{

    public class DoctorConsumerService : IDoctorConsumerService
    {
        private readonly IDoctorDatabaseService _databaseService;

        public DoctorConsumerService(IDoctorDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<DoctorValidationResult> ValidateDoctorAsync(string crm, string password)
        {
            var dbResult = await _databaseService.ValidateDoctorCredentialsAsync(crm, password);

            return new DoctorValidationResult
            {
                IsAuthenticated = dbResult.Found,
                Name = dbResult.Name,
                ErrorMessage = dbResult.ErrorMessage
            };
        }

        public async Task<bool> InsertDoctorAsync(DoctorInsert doctor)
        {
            return await _databaseService.InsertDoctorAsync(doctor);
        }

        public async Task<bool> DoctorAgendaInsertAsync(DoctorAgendaInsert doctorAgenda)
        {
            return await _databaseService.InsertDoctorAgendaAsync(doctorAgenda);
        }
    }
}
