namespace HealthMed.Domain.Results
{
    public class DoctorValidationResult
    {
        public bool IsAuthenticated { get; set; }
        public string? Name { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
