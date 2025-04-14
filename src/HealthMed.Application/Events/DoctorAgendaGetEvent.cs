namespace HealthMed.Application.Events
{
    public class DoctorAgendaGetEvent
    {
        public required string Crm { get; set; }
        public DateTime? DataHora { get; set; }
    }

    public class DoctorAgendaGetEventResponse
    {
        public DateTime? DataHora { get; set; }
        public bool? IsScheduled { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
