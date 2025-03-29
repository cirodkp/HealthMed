namespace HealthMed.Application.Interfaces
{
    public interface IDoctorLoginService
    {
        Task<bool> ValidateDoctorAsync(string crm, string password);
    }
}
