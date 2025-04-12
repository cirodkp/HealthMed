using System.ComponentModel.DataAnnotations;

namespace HealthMed.Application.Models
{
    public record DoctorControllerInsertRequest(
        [Required][MaxLength(10, ErrorMessage = "Tamanho inválido, máximo de 10 caracteres.")] string Crm,
        [Required][MaxLength(255, ErrorMessage = "Tamanho inválido, máximo de 255 caracteres.")] string Name,
        [EmailAddress(ErrorMessage = "Este endereço de e-mail não é válido.")][MaxLength(500, ErrorMessage =
        "Tamanho inválido, máximo de 500 caracteres.")] string Email,
        [Required][MaxLength(1000, ErrorMessage = "Tamanho inválido, máximo de 1000 caracteres.")] string PasswordHash,
        [MaxLength(1000, ErrorMessage = "Tamanho inválido, máximo de 1000 caracteres.")] string KeyMFA);
}
