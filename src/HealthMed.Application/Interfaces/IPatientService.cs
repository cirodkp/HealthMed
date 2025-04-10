using HealthMed.Domain.Entities;


namespace HealthMed.Application.Interfaces;

public interface IPatientService
{
    Task<bool> LoginAsync(DoctorCredentials doctorCredentials);
    Task<Appointment> GetDoctor(Guid Id);
    Task<Appointment> GetAllDoctors();
    Task<Appointment> GetDoctorsBySpeciality(DoctorSpecialtyEnum speaciality);
    Task<Appointment> RegisterAppoitment(Appointment entity);

}
