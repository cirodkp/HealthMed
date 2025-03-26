using HealthMed.Consumer.Doctor.Infra;

namespace HealthMed.Consumer.Doctor.Services
{
    public interface IDoctorService
    {
        Task<bool> ValidateDoctorAsync(string crm, string password);
    }

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
