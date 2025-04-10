using System.ComponentModel.DataAnnotations;

namespace HealthMed.Domain.Entities;

public class Patient
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(11)]
    public string Cpf { get; set; }

    [Required, StringLength(500)]
    public string Email { get; set; }

    [Required, StringLength(1000)]
    public string PasswordHash { get; set; }

    [Required, StringLength(1000)]
    public string MfaKey { get; set; }

    [Required, StringLength(255)]
    public string Name { get; set; }

    public ICollection<Appointment> Appointments { get; set; }

}
