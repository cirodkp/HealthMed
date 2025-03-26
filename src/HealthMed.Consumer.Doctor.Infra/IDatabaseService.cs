namespace HealthMed.Consumer.Doctor.Infra
{
    public interface IDatabaseService
    {

        Task<bool> ValidateDoctorCredentialsAsync(string crm, string password);

    }
}
