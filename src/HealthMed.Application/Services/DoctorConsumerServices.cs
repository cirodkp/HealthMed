using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using HealthMed.Domain.Entities;
using HealthMed.Infra;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Application.Services
{

    public class DoctorConsumerService : IDoctorConsumerService
    {
        private readonly IDatabaseService _databaseService;

        public DoctorConsumerService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> ValidateDoctorAsync(string crm, string password)
        {
            return await _databaseService.ValidateDoctorCredentialsAsync(crm, password);
        }

        public async Task<bool> InsertDoctorAsync(DoctorInsert doctor)
        {
            return await _databaseService.InsertDoctorAsync(doctor);
        }
    }


}
