using System.ComponentModel.DataAnnotations;

namespace HealthMed.Application.Models
{
    public record DoctorAgendaControllerDeleteRequest(
        [Required][MaxLength(10, ErrorMessage = "Tamanho inválido, máximo de 10 caracteres.")] string Crm,
        [Required] DateTime DataHora);
}
