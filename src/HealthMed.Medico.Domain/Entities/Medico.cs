using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Doctor.Domain.Entities
{
    public class Medico
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public string Especialidade { get; set; }
        public string CRM { get; set; }
        public List<HorarioDisponivel> Horarios { get; set; } = new();

        public Medico(string nome, string especialidade, string crm, List<HorarioDisponivel>? horarios = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do médico é obrigatório.");

            if (string.IsNullOrWhiteSpace(especialidade))
                throw new ArgumentException("Especialidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(crm))
                throw new ArgumentException("CRM é obrigatório.");

            Id = Guid.NewGuid();
            Nome = nome;
            Especialidade = especialidade;
            CRM = crm;
            Horarios = horarios ?? new List<HorarioDisponivel>();
        }

        // 🔹 Método para adicionar horários
        public void AtualizarHorarios(List<DateTime> novosHorarios)
        {
            Horarios = novosHorarios.Select(dh => new HorarioDisponivel { DataHora = dh }).ToList();
        }

        public  void Update(string nome, string especialidade, string crm, List<HorarioDisponivel>? horarios = null)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new ArgumentException("Nome do médico é obrigatório.");

            if (string.IsNullOrWhiteSpace(especialidade))
                throw new ArgumentException("Especialidade é obrigatória.");

            if (string.IsNullOrWhiteSpace(crm))
                throw new ArgumentException("CRM é obrigatório.");

            Id = Guid.NewGuid();
            Nome = nome;
            Especialidade = especialidade;
            CRM = crm;
            Horarios = horarios ?? new List<HorarioDisponivel>();
        }
    }
}
