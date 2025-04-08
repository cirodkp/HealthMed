namespace HealthMed.Application.Results
{
    public record AdminCredentialsResponse(string token);
    public record DoctorCredentialsResponse(string token);
    public record PatientCredentialsResponse(string token);
}
