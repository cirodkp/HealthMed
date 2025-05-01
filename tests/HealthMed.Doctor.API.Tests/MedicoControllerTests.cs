using Bogus;
using FluentAssertions;
using HealthMed.Doctor.API.Tests.Abstractions;
using HealthMed.Doctor.Application.ViewModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace HealthMed.Doctor.API.Tests
{
    public class MedicoControllerTests : BaseFunctionalTests
    {
        private readonly Faker _faker;
        private readonly FunctionalTestWebAppFactory _testsFixture;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public MedicoControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
        {
            _testsFixture = factory;
            _faker = new Faker(locale: "pt_BR");
        }

        [Fact(DisplayName = "Deve inserir um novo médico e retornar PublishResponse")]
        public async Task Insert_DeveRetornar200()
        {
            var request = new InsertMedicoRequest(

                "Dr. Teste",
                 "Cardiologia",
                 $"CRM{Guid.NewGuid().ToString("N")[..6]}",
                 new List<HorarioDto>
                {
                    new() { DataHora = DateTime.Now.AddDays(1), Ocupado = false }
                }
            );

            var response = await HttpClient.PostAsJsonAsync("api/medico/insert", request);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<PublishResponse>();
            result!.Message.Should().Be("Cadastro em processamento.");
        }

        [Fact(DisplayName = "Deve atualizar um médico e retornar PublishResponse")]
        public async Task Update_DeveRetornar200()
        {
            var medico = await EnsureAnyMedicoExists();

            var updateRequest = new UpdateMedicoRequest(

                 medico.Id,
                "Dr. Atualizado",
                medico.Especialidade,
                medico.CRM,
                 new List<HorarioDto>
                {
                    new() { DataHora = DateTime.Now.AddDays(2), Ocupado = false }
                }
            );

            var response = await HttpClient.PutAsJsonAsync("api/medico/update", updateRequest);

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<PublishResponse>();
            result!.Message.Should().Be("Atualização em processamento.");
        }

        [Fact(DisplayName = "Deve excluir um médico e retornar PublishResponse")]
        public async Task Delete_DeveRetornar200()
        {
            var medico = await EnsureAnyMedicoExists();

            var response = await HttpClient.DeleteAsync($"api/medico/delete?id={medico.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var result = await response.Content.ReadFromJsonAsync<PublishResponse>();
            result!.Message.Should().Be("Exclusão em processamento.");
        }

        [Fact(DisplayName = "Deve retornar todos os médicos")]
        public async Task GetAll_DeveRetornar200()
        {
            var response = await HttpClient.GetAsync("api/medico/getall");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var medicos = await response.Content.ReadFromJsonAsync<List<MedicoResponse>>();
            medicos.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve buscar médico por CRM")]
        public async Task GetByCRM_DeveRetornar200()
        {
            var medico = await EnsureAnyMedicoExists();

            var response = await HttpClient.GetAsync($"api/medico/getbycrm?crm={medico.CRM}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var fetched = await response.Content.ReadFromJsonAsync<MedicoResponse>();
            fetched!.CRM.Should().Be(medico.CRM);
        }

        [Fact(DisplayName = "Deve buscar médico por ID")]
        public async Task GetById_DeveRetornar200()
        {
            var medico = await EnsureAnyMedicoExists();

            var response = await HttpClient.GetAsync($"api/medico/getbyid?id={medico.Id}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var fetched = await response.Content.ReadFromJsonAsync<MedicoResponse>();
            fetched!.Id.Should().Be(medico.Id);
        }

        private async Task<MedicoResponse> EnsureAnyMedicoExists()
        {
            //Act
            var response = await HttpClient.GetAsync("api/medico/getall");
            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var content = await response.Content.ReadAsStringAsync();

            var lista = JsonSerializer.Deserialize<List<MedicoResponse>>(content, jsonOptions)!;


            if (lista is not null && lista.Any())
                return lista.First();

            // Inserir novo médico se não houver nenhum
            var insertRequest = new InsertMedicoRequest(

                "Dr. AutoSeed",
                 "Cardiologia",
                $"CRM{Guid.NewGuid().ToString("N")[..6]}",
                 new List<HorarioDto>
                {
                    new() { DataHora = DateTime.Now.AddDays(1), Ocupado = false }
                }
            );

            var insertResponse = await HttpClient.PostAsJsonAsync("api/medico/insert", insertRequest);
            insertResponse.EnsureSuccessStatusCode();

            var responseretry = await HttpClient.GetAsync("api/medico/getall");
            var contentret = await responseretry.Content.ReadAsStringAsync();

            var atualizada = JsonSerializer.Deserialize<List<MedicoResponse>>(contentret, jsonOptions)!;


          

            return atualizada!.First();
        }
    }
}