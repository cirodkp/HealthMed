using HealthMed.Patient.Application.Events;
using HealthMed.Patient.Application.Interfaces;
using HealthMed.Patient.Application.ViewModels;
using HealthMed.Patient.Domain.Interfaces;
using HealthMed.Patient.Domain.Validations;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.UseCases
{
    public class UpdatePacienteUseCase([FromServices] IPacientePublisher pacientetPublisher /* Serviço dedicado para publicar */,
        IPacienteRepository pacienteRepository) : IUpdatePacienteUseCase 
    {
        public async Task<PublishResponse> Execute(UpdatePacienteRequest updatePacienteRequest)
        {
            var contact = await pacienteRepository.ObterPorIdAsync(updatePacienteRequest.Id);
            if (contact == null) throw new ApplicationException("Não foi possível localizar o cadastro do paciente informado.");

            if (!PacienteValidations.IsValidNome(updatePacienteRequest.Nome))
                throw new ArgumentException("O nome é obrigatório.");

            if (!PacienteValidations.IsValidEmail(updatePacienteRequest.Email))
                throw new ArgumentException("Formato de e-mail inválido.");

             

            await pacientetPublisher.PublishUpdatePacienteAsync(new UpdatePacienteEvent
            {
                Id = updatePacienteRequest.Id,
                Nome = updatePacienteRequest.Nome,
                Email = updatePacienteRequest.Email ,
                Cpf   = updatePacienteRequest.Cpf
            });

            return new PublishResponse
            {
                Message = "Atualização em processamento.",
                Data = new
                {
                    updatePacienteRequest.Id,
                    updatePacienteRequest.Nome,
                    updatePacienteRequest.Email,
                    updatePacienteRequest.Cpf
                }
            };
        }
    }
}

 
