using System.ComponentModel.DataAnnotations;

namespace HealthMed.Domain.Entities;

public class Doctor
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(10)]
    public string Crm { get; set; }

    [Required, StringLength(500)]
    public string Email { get; set; }

    [Required, StringLength(1000)]
    public string PasswordHash { get; set; }

    [Required, StringLength(1000)]
    public string MfaKey { get; set; }

    [Required, StringLength(255)]
    public string Name { get; set; }

    public ICollection<DoctorSchedule> Schedules { get; set; }
    public DoctorSpecialtyEnum Speciality { get; set; }
}

public enum DoctorSpecialtyEnum
{
    GeneralPractitioner = 1,
    Cardiologist = 2,
    Dermatologist = 3,
    Neurologist = 4,
    Orthopedist = 5,
    Pediatrician = 6,
    Gynecologist = 7,
    Psychiatrist = 8,
    Urologist = 9,
    Endocrinologist = 10
}