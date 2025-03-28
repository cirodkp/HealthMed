namespace HealthMed.Infra
{
    public interface IDatabaseService
    {

        Task<bool> ValidateDoctorCredentialsAsync(string crm, string password);

    }
}
