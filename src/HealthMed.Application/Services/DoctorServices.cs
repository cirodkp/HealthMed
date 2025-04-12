using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;


namespace HealthMed.Application.Services;
public class DoctorService : IDoctorService
{
    private readonly IGenericRepository _genericRepository;

    public DoctorService(IGenericRepository doctorScheduleRepository)
    {
        _genericRepository = doctorScheduleRepository;
    }

    public async Task<object> LoginAsync(DoctorCredentials doctorCredentials)
    {
        var command = "SELECT COUNT(1) FROM medicos WHERE crm = '{v}' AND pass_hash = '{password}'";
        command.Replace("{crm}", doctorCredentials.Crm);
        command.Replace("{password}", doctorCredentials.Password);
        return await _genericRepository.ExecuteQuery<bool>(command);
    }


    public Task<DoctorSchedule> RegisterScheduleAsync(DoctorSchedule schedule)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> AcceptAppointment(Guid id, AppointmentStatus status, string? statusMessage)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> CancelAppointment(Guid id, AppointmentStatus status, string statusMessage)
    {
        throw new NotImplementedException();
    }

    public Task<DoctorSchedule> GetAllSheduleAsync(DateTime dateStart, DateTime dateEnd)
    {
        throw new NotImplementedException();
    }

    public Task<DoctorSchedule> UpdateSheduleAsync(Guid id, DoctorSchedule schedule)
    {
        throw new NotImplementedException();
    }
}
