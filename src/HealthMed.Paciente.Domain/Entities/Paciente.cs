using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthMed.Patient.Domain.Entities
{
   public class Paciente
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public Paciente() { }

        public Paciente(string nome,string cpf, string email)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }

        public void Update(string nome, string cpf, string email)
        {
            Nome = nome;
            Cpf = cpf;
            Email = email;
        }
    }
}
