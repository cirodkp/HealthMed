using HealthMed.Application.Interfaces;
using HealthMed.Infra;

namespace HealthMed.Application.Services
{

    public class DoctorService : IDoctorService
    {
        private readonly IDatabaseService _databaseService;

        public DoctorService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> ValidateDoctorAsync(string crm, string password)
        {
            return await _databaseService.ValidateDoctorCredentialsAsync(crm, password);
        }
    }
}
