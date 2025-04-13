using System.Security.Cryptography;
using System.Text;
using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Application.Services
{
    public class DoctorAPIInsertService([FromServices] IDoctorPublisher doctorPublisher) : IDoctorControllerInsertService
    {
        public async Task<SendResponseAsync> Execute(DoctorControllerInsertRequest insertDoctorRequest)
        {
            //TODO: Validação

            string passwordHash = PasswordService.CalculaPasswordHash_Sha512(insertDoctorRequest.PasswordHash, insertDoctorRequest.Crm);

            await doctorPublisher.SendInsertDoctorAsync(new InsertDoctorEvent
            {
                Crm = insertDoctorRequest.Crm,
                Name = insertDoctorRequest.Name,
                Email = insertDoctorRequest.Email,
                PasswordHash = passwordHash,
                KeyMFA = insertDoctorRequest.KeyMFA
            });

            return new SendResponseAsync
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

    public class DoctorAPILoginService([FromServices] IDoctorPublisher doctorPublisher) : IDoctorControllerLoginService
    {
        public async Task<SendResponseAsync> Execute(DoctorControllerLoginRequest loginDoctorRequest)
        {
            //TODO: Validação

            string passwordHash = PasswordService.CalculaPasswordHash_Sha512(loginDoctorRequest.Password, loginDoctorRequest.Crm);

            var result = await doctorPublisher.RequestLoginDoctorSync(new DoctorLoginEvent
            {
                Crm = loginDoctorRequest.Crm,
                PasswordHash = passwordHash
            });

            return new SendResponseAsync
            {
                Message = "Atualização em processamento.",
                Data = new
                {
                    loginDoctorRequest.Crm
                }
            };
        }

    }
}
