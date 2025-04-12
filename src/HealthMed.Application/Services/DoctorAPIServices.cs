using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Models;
using HealthMed.Application.Results;
using Microsoft.AspNetCore.Mvc;

namespace HealthMed.Application.Services
{
    public class DoctorAPIInsertService([FromServices] IDoctorPublisher doctorPublisher) : IInsertDoctorService
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
