using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Consultation.Domain.Entities
{
    public class Consulta
    {
        public int Id { get; set; } 
        public string CpfPaciente { get; set; } = string.Empty;
        public string NomePaciente { get; set; } = string.Empty;
        public string CrmMedico { get; set; } = string.Empty;
        public DateTime DataHora { get; set; }
        public string Status { get; set; } = "Pendente"; // Aceita, Recusada, Cancelada
        public string? Justificativa { get; set; }

        // Construtor com todos os campos obrigatórios
        public Consulta(int id, string cpfPaciente, string nomePaciente, string crmMedico, DateTime dataHora)
        {
           
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
          
            CpfPaciente = cpfPaciente;
            NomePaciente = nomePaciente;
            CrmMedico = crmMedico;
            DataHora = dataHora;
            Status = status;
            Justificativa = justificativa;
        }

        public void Update(int id, string status, string justificativa)
        {
            if (string.IsNullOrWhiteSpace(Status))
                throw new ArgumentException("Status é obrigatório.");

            this.Id = id;
            this.Status = status;
            this.Justificativa = justificativa;  
        }
    }
}
