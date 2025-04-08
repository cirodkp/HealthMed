using HealthMed.Application.Commands;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IAuthenticationAdminService
    {
        Task<AdminCredentialsResponse> Execute(AdminAuthenticationCommand command);
    }
    public interface IAuthenticationDoctorService
    {
        Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand command);
    }
    public interface IAuthenticationPatientService
    {
        Task<PatientCredentialsResponse> Execute(PatientAuthenticationCommand command);
    }
}
