using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using HealthMed.Infra;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Application.Services
{

    public class DoctorService : IDoctorLoginService
    {
        private readonly IDatabaseService _databaseService;

        public DoctorService(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        public async Task<bool> ValidateDoctorAsync(string crm, string password)
        {
            return await _databaseService.ValidateDoctorCredentialsAsync(crm, password);
        }
    }

    public class InsertDoctorService([FromServices] IDoctorPublisher doctorPublisher) : IInsertDoctorService
    {
        public async Task<PublishAsyncResponse> Execute(InsertDoctorRequest insertDoctorRequest)
        {
            //TODO: Validação

            await doctorPublisher.PublishInsertDoctorAsync(new InsertDoctorEvent
            {
                Crm = insertDoctorRequest.Crm,
                Name = insertDoctorRequest.Name,
                Email = insertDoctorRequest.Email,
                PasswordHash = insertDoctorRequest.PasswordHash,
                KeyMFA = insertDoctorRequest.KeyMFA
            });

            return new PublishAsyncResponse
            {
                Message = "Atualização em processamento.",
                Data = new
                {
                    insertDoctorRequest.Crm,
                    insertDoctorRequest.Name,
                    insertDoctorRequest.Email
                }
            };
        }
    }
}
