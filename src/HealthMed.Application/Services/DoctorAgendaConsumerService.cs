using HealthMed.Application.Interfaces;
using HealthMed.Domain.Entities;
using HealthMed.Infra;

namespace HealthMed.Application.Services
{
    public class DoctorAgendaConsumerService : IDoctorAgendaConsumerService
    {

        private readonly IDoctorDatabaseService _databaseService;

        public DoctorAgendaConsumerService(IDoctorDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> DoctorAgendaInsertAsync(DoctorAgendaInsert doctorAgendaInsert)
        {
            return await _databaseService.InsertDoctorAgendaAsync(doctorAgendaInsert);
        }
    }
}
