using HealthMed.Application.Events;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorPublisher
    {
        Task SendInsertDoctorAsync(InsertDoctorEvent message);
        Task<bool> RequestLoginDoctorSync(DoctorLoginEvent doctorLoginEvent);
    }
}
