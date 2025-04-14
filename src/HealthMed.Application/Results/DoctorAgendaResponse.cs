namespace HealthMed.Application.Results
{
    public record DoctorAgendaResponse(
         string Crm,
         DateTime DataHora,
         bool IsScheduled
    );
}
