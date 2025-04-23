using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Agenda.Domain.Entities
{
    public class HorarioDisponivel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid MedicoId { get; set; }
        public DateTime DataHora { get; set; }
        public bool Ocupado { get; set; } = false;
    }

}