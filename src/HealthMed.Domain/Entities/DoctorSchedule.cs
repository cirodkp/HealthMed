using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HealthMed.Domain.Entities;
public class DoctorSchedule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DoctorId { get; set; }

    [ForeignKey("DoctorId")]
    public Doctor Doctor { get; set; }

    [Required]
    public DateTime ScheduleDate { get; set; }

    [Required]
    public TimeSpan ScheduleTime { get; set; }

    public ICollection<Appointment> Appointments { get; set; }
}

