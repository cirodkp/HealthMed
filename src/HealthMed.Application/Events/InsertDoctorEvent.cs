namespace HealthMed.Application.Events
{
    public class InsertDoctorEvent
    {
        public required string Crm { get; set; }
        public required string Name { get; set; }
        public string? Email { get; set; }
        public required string PasswordHash { get; set; }
        public string? KeyMFA { get; set; }
    }

    public class InsertDoctorEventResponse
    {
        public required int Id { get; set; }
        public required string Crm { get; set; }
        public required string Name { get; set; }
        public required string Status { get; set; }
        
    }
}
