namespace HealthMed.Infra.Models
{
    public class DoctorDbValidationResult
    {
        public bool Found { get; set; }
        public string? Name { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
