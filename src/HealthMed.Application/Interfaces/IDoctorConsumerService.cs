using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorConsumerService
    {
        Task<bool> ValidateDoctorAsync(string crm, string password);
        Task<bool> InsertDoctorAsync(DoctorInsert doctorInsert);
    }
}
