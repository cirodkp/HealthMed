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
    public class DoctorAgendaControllerInsertService([FromServices] IDoctorPublisher doctorPublisher) : IDoctorAgendaControllerInsertService
    {
        public async Task<SendResponseAsync> Execute(DoctorAgendaControllerInsertRequest insertDoctorAgendaRequest)
        {
            //TODO: Validação

            await doctorPublisher.SendInsertDoctorAgendaAsync(new DoctorAgendaInsertEvent
            {
                Crm = insertDoctorAgendaRequest.Crm,
                DataHora = insertDoctorAgendaRequest.DataHora
            });

            return new SendResponseAsync
            {
                Message = "Inclusão em processamento.",
                Data = new
                {
                    insertDoctorAgendaRequest.Crm,
                    insertDoctorAgendaRequest.DataHora
                }
            };
        }

    }

    public class DoctorAgendaControllerGetService([FromServices] IDoctorPublisher doctorPublisher) : IDoctorAgendaControllerGetService
    {
        public async Task<IEnumerable<DoctorAgendaResponse>> Execute(DoctorAgendaControllerGetRequest request)
        {
            // Validação básica
            if (string.IsNullOrWhiteSpace(request.Crm))
                throw new ArgumentException("CRM é obrigatório.");

            // Chamada para o publisher (sincrônica, como no login)
            var agendas = await doctorPublisher.RequestDoctorAgendaGetSync(new DoctorAgendaGetEvent
            {
                Crm = request.Crm
            });

            if (agendas.Any(a => !string.IsNullOrWhiteSpace(a.ErrorMessage)))
                throw new KeyNotFoundException(agendas.First().ErrorMessage ?? "Médico não encontrado.");

            return agendas.Select(a => new DoctorAgendaResponse(
                Crm: request.Crm,
                DataHora: a.DataHora ?? DateTime.MinValue,
                IsScheduled: a.IsScheduled ?? false
            ));
        }
    }
}
