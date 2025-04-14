using HealthMed.Application.Events;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorPublisher
    {
        Task SendInsertDoctorAsync(InsertDoctorEvent message);
        Task<DoctorLoginEventResponse> RequestLoginDoctorSync(DoctorLoginEvent doctorLoginEvent);
        Task<IEnumerable<DoctorAgendaGetEventResponse>> RequestDoctorAgendaGetSync(DoctorAgendaGetEvent doctorAgendaGetEvent);
    }
}
