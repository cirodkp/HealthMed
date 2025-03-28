using HealthMed.Application.Commands;
using HealthMed.Application.Results;

namespace HealthMed.Application.Interfaces
{
    public interface IAuthenticationService
    {
        Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand command);
    }
}
