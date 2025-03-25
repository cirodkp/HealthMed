using HealthAndMed.Application.Commands;
using HealthMed.Application.ViewModels;

namespace HealthAndMed.Application.Interfaces
{
    public interface IAuthenticationUseCase
    {
        Task<DoctorCredentialsResponse> Execute(DoctorAuthenticationCommand command);
    }
}
