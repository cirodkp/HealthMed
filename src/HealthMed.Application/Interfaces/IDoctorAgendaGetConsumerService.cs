using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAgendaGetConsumerService
    {
        Task<IEnumerable<DoctorAgendaGet>> GetAgendasByCrmAsync(string crm);
    }
}
