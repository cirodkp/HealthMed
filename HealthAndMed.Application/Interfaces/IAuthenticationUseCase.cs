using HealthAndMed.Application.Commands;

namespace HealthAndMed.Application.Interfaces
{
    public interface IAuthenticationUseCase
    {
        public string GetToken(DoctorAuthenticationCommand command);
    }
}
