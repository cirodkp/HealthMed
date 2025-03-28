namespace HealthMed.Application.Interfaces
{
    public interface IDoctorService
    {
        Task<bool> ValidateDoctorAsync(string crm, string password);
    }
}
