namespace HealthMed.Application.Events
{
    public class DoctorAgendaInsertEvent
    {
        public required string Crm { get; set; }
        public required DateTime DataHora { get; set; }
    }

    public class DoctorAgendaGetEvent
    {
        public required string Crm { get; set; }
        public DateTime? DataHora { get; set; }
    }

    public class DoctorAgendaGetEventResponse
    {
        public required string Crm { get; set; }
        public DateTime? DataHora { get; set; }
        public bool? IsScheduled { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
