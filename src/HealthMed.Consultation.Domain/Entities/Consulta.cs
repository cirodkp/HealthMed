using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Domain.Entities
{
    public class Consulta
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CpfPaciente { get; set; } = string.Empty;
        public string NomePaciente { get; set; } = string.Empty;
        public string CrmMedico { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "Pendente"; // Aceita, Recusada, Cancelada
        public string? Justificativa { get; set; }

        // Construtor com todos os campos obrigatórios
        public Consulta(Guid id, string cpfPaciente, string nomePaciente, string crmMedico, DateTime dataHora)
        {
            Id = Guid.NewGuid();
            Id = id;
            CpfPaciente = cpfPaciente;
            NomePaciente = nomePaciente;
            CrmMedico = crmMedico;
            DataHora = dataHora;
            Status = "Pendente";
        }

        // Construtor adicional com justificativa opcional (caso queira flexibilidade)
        public Consulta(string cpfPaciente, string nomePaciente, string crmMedico, DateTime dataHora, string status, string? justificativa = null)
        {
            Id = Guid.NewGuid();
            CpfPaciente = cpfPaciente;
            NomePaciente = nomePaciente;
            CrmMedico = crmMedico;
            DataHora = dataHora;
            Status = status;
            Justificativa = justificativa;
        }


        public void Update(Guid Id, string Status, string Justificativa)
        {
            if (string.IsNullOrWhiteSpace(Status))
                throw new ArgumentException("Status é obrigatório.");

            if (string.IsNullOrWhiteSpace(Justificativa))
                throw new ArgumentException("Justificativa é obrigatória.");
 

            Id = Guid.NewGuid();
            Status = Status;
            Justificativa = Justificativa;
          
             
        }

    }
}
