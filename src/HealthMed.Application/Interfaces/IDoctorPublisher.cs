using HealthMed.Application.Events;

namespace HealthMed.Application.Interfaces
{
    public interface IDoctorPublisher
    {
        Task PublishInsertDoctorAsync(InsertDoctorEvent message);
    }
}
