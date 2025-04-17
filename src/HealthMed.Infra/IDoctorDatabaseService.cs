using HealthMed.Domain.Entities;
using HealthMed.Infra.Models;

namespace HealthMed.Infra
{
    public interface IDoctorDatabaseService
    {

        Task<DoctorDbValidationResult> ValidateDoctorCredentialsAsync(string crm, string password);

        Task<bool> InsertDoctorAsync(DoctorInsert doctor);

        Task<bool> InsertDoctorAgendaAsync(DoctorAgendaInsert doctorAgenda);

        Task<IEnumerable<DoctorAgendaGet>> GetDoctorAgendasByCrmAsync(string crm);

    }
}
