using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;
using HealthMed.Infra;
using Npgsql;

namespace HealthMed.Application.Services;
public class PatientService : IPatientService
{
    private readonly IDatabaseService _databaseService;
    private readonly IGenericRepository<DoctorSchedule> _doctorScheduleRepository;

    public PatientService(
        IDatabaseService databaseService,
        IGenericRepository<Doctor> doctorRepository,
        IGenericRepository<DoctorSchedule> doctorScheduleRepository)
    {
        _databaseService = databaseService;
        _doctorScheduleRepository = doctorScheduleRepository;
    }

    public async Task<bool> LoginAsync(DoctorCredentials doctorCredentials)
    {
        using var conn = await _databaseService.CreateConnectionAsync();
        var command = new NpgsqlCommand("SELECT COUNT(1) FROM medicos WHERE crm = @crm AND pass_hash = @password", (NpgsqlConnection)conn);
        command.Parameters.AddWithValue("crm", doctorCredentials.Crm);
        command.Parameters.AddWithValue("password", doctorCredentials.Password);
        var result = await command.ExecuteScalarAsync();
        return (long)result > 0;
    }


    public Task<Appointment> GetAllDoctors()
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> GetDoctor(Guid Id)
    {
        throw new NotImplementedException();
    }

    public Task<Appointment> GetDoctorsBySpeciality(DoctorSpecialtyEnum speaciality)
    {
        throw new NotImplementedException();
    }
    
    public Task<Appointment> RegisterAppoitment(Appointment entity)
    {
        throw new NotImplementedException();
    }
}
