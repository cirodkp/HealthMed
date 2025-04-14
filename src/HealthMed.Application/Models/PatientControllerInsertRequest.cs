using System.ComponentModel.DataAnnotations;

namespace HealthMed.Application.Models
{
    public record PatientControllerInsertRequest(
        [Required][MaxLength(11, ErrorMessage = "Tamanho inválido, máximo de 11 caracteres.")] string CPF,
        [Required][MaxLength(255, ErrorMessage = "Tamanho inválido, máximo de 255 caracteres.")] string Name,
        [EmailAddress(ErrorMessage = "Este endereço de e-mail não é válido.")][MaxLength(500, ErrorMessage =
        "Tamanho inválido, máximo de 500 caracteres.")] string Email,
        [Required][MaxLength(1000, ErrorMessage = "Tamanho inválido, máximo de 1000 caracteres.")] string Password,
        [MaxLength(1000, ErrorMessage = "Tamanho inválido, máximo de 1000 caracteres.")] string KeyMFA);
}
