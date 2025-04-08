namespace HealthMed.Application.Commands
{
    public class AdminAuthenticationCommand
    {
        public string User { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class DoctorAuthenticationCommand
    {
        public string Crm { get; set; } = "";
        public string Password { get; set; } = "";
    }
    public class PatientAuthenticationCommand
    {
        public string Cpf { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
