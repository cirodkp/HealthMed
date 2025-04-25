using HealthMed.Patient.Application.Interfaces;
using HealthMed.Patient.Application.ViewModels;
using HealthMed.Patient.Domain.Entities;
using HealthMed.Patient.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Patient.Application.UseCases
{
    public class GetPacienteUseCase(IPacienteRepository  pacienteRepository) : IGetPacienteUseCase
    {
        public async Task<PacienteResponse?> ObterPorCpfAsync(string cpf)
        {
            var paciente = await pacienteRepository.ObterPorCpfAsync(cpf);
            if (paciente is null)
                throw new ApplicationException("Contato não encontrado!");
            return new PacienteResponse(paciente.Id, paciente.Nome, paciente.Cpf, paciente.Email);
        }

        public async Task<PacienteResponse?> ObterPorIdAsync(int id)
        {
            var paciente = await pacienteRepository.ObterPorIdAsync(id);
            if (paciente is null)
                throw new ApplicationException("Contato não encontrado!");
            return new PacienteResponse(paciente.Id, paciente.Nome, paciente.Cpf, paciente.Email);
        }

        public async Task<List<PacienteResponse>> ObterTodosAsync()
        {
            List<Paciente> result = [];
            result = await pacienteRepository.ObterTodosAsync();
            var mapped = result.Select(x => new PacienteResponse(x.Id, x.Nome, x.Cpf, x.Email  )).ToList();
            return mapped;
        }
    }
}
