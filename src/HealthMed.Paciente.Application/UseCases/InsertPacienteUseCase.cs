using HealthMed.Patient.Application.Events;
using HealthMed.Patient.Application.Interfaces;
using HealthMed.Patient.Application.ViewModels;
using HealthMed.Patient.Domain.Entities;
using HealthMed.Patient.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.UseCases
{
  public   class InsertPacienteUseCase([FromServices] IPacientePublisher pacientePublisher /* Serviço dedicado para publicar */) : IInsertPacienteUseCase
    {
        public async Task<PublishResponse> Execute(InsertPacienteRequest insertPacienteRequest)
        {
            if (!PacienteValidations.IsValidNome(insertPacienteRequest.Nome))
                throw new ArgumentException("O nome é obrigatório.");

            if (!PacienteValidations.IsValidEmail(insertPacienteRequest.Email))
                throw new ArgumentException($"Formato de e-mail inválido.");



            var paciente = new Paciente(insertPacienteRequest.Nome, insertPacienteRequest.Cpf, insertPacienteRequest.Email);

            // Cria a mensagem e publica na fila
            await pacientePublisher.PublishInsertPacienteAsync(new InsertPacienteEvent
            {
                Nome = insertPacienteRequest.Nome,
                Email = insertPacienteRequest.Email,
                Cpf = insertPacienteRequest.Cpf
            });

            return new PublishResponse
            {
                Message = "Cadastro em processamento.",
                Data = new
                {
                    insertPacienteRequest.Nome,
                    insertPacienteRequest.Email,
                    insertPacienteRequest.Cpf
                }
            };
        }
    }
 
    
}
