using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Domain.Interfaces;

namespace HealthMed.Application.Services;
public class PatientService : IPatientService
{
    private readonly IGenericRepository _genericRepository;

    public PatientService(IGenericRepository genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<object> LoginAsync(PatientCredentials patientCredentials)
    {
        var command = "SELECT COUNT(1) FROM medicos WHERE crm = '{cpf}' AND pass_hash = '{password}'";
        command.Replace("{cpf}", patientCredentials.CPF);
        command.Replace("{password}", patientCredentials.Password);
        return await _genericRepository.ExecuteQuery<bool>(command);
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
