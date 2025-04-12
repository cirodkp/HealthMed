using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMed.Domain.Entities;
public class Appointment
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("DoctorId")]
    public Patient DoctorId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; }

    public AppointmentStatus Status { get; set; }
    public string StatusMessage { get; set; }
}

public enum AppointmentStatus
{
    Scheduled = 1,
    Completed = 2,
    Cancelled = 3,
    Rejected = 5
}