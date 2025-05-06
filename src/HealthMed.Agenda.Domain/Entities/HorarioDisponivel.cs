namespace HealthMed.Agenda.Domain.Entities
{
    public class HorarioDisponivel
    {
        public HorarioDisponivel()
        {
            
        }
        public HorarioDisponivel(int medicoId, DateTime dataHora, bool ocupado = false)
        {
            this.MedicoId = medicoId;
            this.DataHora = dataHora;
            this.Ocupado = ocupado;
        }
        public int Id { get; set; }  
        public int MedicoId { get; set; }
        public DateTime DataHora { get; set; }
        public bool Ocupado { get; set; } = false;

        public void Update(DateTime dataHora)
        {
            this.DataHora = dataHora;
        }
    }

}