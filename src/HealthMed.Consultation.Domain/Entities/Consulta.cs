using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Domain.Entities
{
    public class Consulta
    {
        private static readonly string[] StatusPermitidos = { "Pendente", "Aceita", "Recusada", "Cancelada" };

        public int Id { get; set; }
        public string CpfPaciente { get; set; } = string.Empty;
        public string NomePaciente { get; set; } = string.Empty;
        public string CrmMedico { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "Pendente";
        public string? Justificativa { get; set; }

        public Consulta(int id, string cpfPaciente, string nomePaciente, string crmMedico, DateTime dataHora)
        {
            Id = id;
            CpfPaciente = cpfPaciente;
            NomePaciente = nomePaciente;
            CrmMedico = crmMedico;
            DataHora = dataHora;
            Status = "Pendente";
        }

        public Consulta(string cpfPaciente, string nomePaciente, string crmMedico, DateTime dataHora, string status, string? justificativa = null)
        {
            if (!StatusPermitidos.Contains(status))
                throw new ArgumentException($"Status inválido. Os status permitidos são: {string.Join(", ", StatusPermitidos)}");

            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status é obrigatório.");
 

            CpfPaciente = cpfPaciente;
            NomePaciente = nomePaciente;
            CrmMedico = crmMedico;
            DataHora = dataHora;
            Status = status;
            Justificativa = justificativa;
        }

        public void Update(int id, string status, string justificativa, DateTime dataHora)
        {
            if (string.IsNullOrWhiteSpace(status))
                throw new ArgumentException("Status é obrigatório.");

            if (!StatusPermitidos.Contains(status))
                throw new ArgumentException($"Status inválido. Os status permitidos são: {string.Join(", ", StatusPermitidos)}");

            if (string.IsNullOrWhiteSpace(justificativa))
                throw new ArgumentException("Justificativa é obrigatória.");

            Id = id;
            Status = status;
            Justificativa = justificativa;
            DataHora = dataHora;
        }
    }
}
