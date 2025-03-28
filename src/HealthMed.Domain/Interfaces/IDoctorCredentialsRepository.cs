using HealthMed.Domain.Entities;

namespace HealthMed.Domain.Interfaces
{
    public interface IDoctorCredentialsRepository : IDisposable
    {
        Task<DoctorCredentials?> GetByCrmAsync(string crm);
        void Save(DoctorCredentials contact);
        void Update(DoctorCredentials contact);
        void Delete(DoctorCredentials contact);
    }
}
