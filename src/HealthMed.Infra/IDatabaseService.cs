using HealthMed.Domain.Entities;

namespace HealthMed.Infra
{
    public interface IDatabaseService
    {

        Task<bool> ValidateDoctorCredentialsAsync(string crm, string password);

        Task<bool> InsertDoctorAsync(DoctorInsert doctor);

    }
}
