using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Application.Models
{
    public record PatientAgendaControllerInsertRequest(
    [Required]
    [MaxLength(11, ErrorMessage = "Tamanho inválido, máximo de 11 caracteres.")]
    [MinLength(11, ErrorMessage = "Tamanho inválido, mínimo de 11 caracteres.")] string Cpf,
    [Required] DateTime DataHora);
}
