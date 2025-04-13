using HealthMed.Domain.Entities;
using HealthMed.Domain.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorConsumerService
    {
        Task<DoctorValidationResult> ValidateDoctorAsync(string crm, string password);
        Task<bool> InsertDoctorAsync(DoctorInsert doctorInsert);
    }
}
