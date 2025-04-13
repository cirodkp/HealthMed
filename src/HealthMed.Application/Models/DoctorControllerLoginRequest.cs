using System.ComponentModel.DataAnnotations;

namespace HealthMed.Application.Models
{
    public record DoctorControllerLoginRequest(
        [Required][MaxLength(10, ErrorMessage = "Tamanho inválido, máximo de 10 caracteres.")] string Crm,
        [Required][MaxLength(1000, ErrorMessage = "Tamanho inválido, máximo de 1000 caracteres.")] string Password);
}
