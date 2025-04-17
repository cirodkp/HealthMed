using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Infra;

namespace HealthMed.Application.Services
{
    public class DoctorAgendaGetConsumerService : IDoctorAgendaGetConsumerService
    {

        private readonly IDoctorDatabaseService _databaseService;

        public DoctorAgendaGetConsumerService(IDoctorDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<IEnumerable<DoctorAgendaGet>> GetAgendasByCrmAsync(string crm)
        {
            return await _databaseService.GetDoctorAgendasByCrmAsync(crm);
        }
    }
}
