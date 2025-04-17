using HealthMed.Domain.Entities;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorAgendaConsumerService
    {
        Task<bool> DoctorAgendaInsertAsync(DoctorAgendaInsert doctorAgendaInsert);
    }
}
