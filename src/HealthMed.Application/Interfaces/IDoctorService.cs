using HealthMed.Domain.Entities;


namespace HealthMed.Application.Interfaces;
public interface IDoctorService
{
    Task<object> LoginAsync(DoctorCredentials doctorCredentials);
    Task<DoctorSchedule> RegisterScheduleAsync(DoctorSchedule schedule);
    Task<DoctorSchedule> GetAllSheduleAsync(DateTime dateStart, DateTime dateEnd);
    Task<DoctorSchedule> UpdateSheduleAsync(Guid id, DoctorSchedule schedule);
    Task<Appointment> AcceptAppointment(Guid id, AppointmentStatus status, string? statusMessage);
    Task<Appointment> CancelAppointment(Guid id, AppointmentStatus status, string statusMessage);
}
