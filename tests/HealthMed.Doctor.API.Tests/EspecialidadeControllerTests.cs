using Bogus;
using FluentAssertions;
using HealthMed.Doctor.API.Tests.Abstractions;
using HealthMed.Doctor.Application.ViewModels;
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
    public class EspecialidadeControllerTests : BaseFunctionalTests
    {
        private readonly Faker _faker;
        private readonly FunctionalTestWebAppFactory _testsFixture;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        public EspecialidadeControllerTests(FunctionalTestWebAppFactory factory) : base(factory)
        {
            _testsFixture = factory;
            _faker = new Faker(locale: "pt_BR");
        }

        [Fact(DisplayName = "Deve retornar 200 OK ao listar todas as especialidades")]
        public async Task ListarTodasAsync_DeveRetornar200()
        {
            var response = await HttpClient.GetAsync("api/especialidade/listar-todas");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var especialidades = await response.Content.ReadFromJsonAsync<List<EspecialidadeResponse>>();
            especialidades.Should().NotBeNull();
        }

        [Fact(DisplayName = "Deve retornar 200 OK ao buscar especialidade por nome")]
        public async Task GetByNomeAsync_DeveRetornar200()
        {
            var nome = "Cardiologia";

            var response = await HttpClient.GetAsync($"api/especialidade/get-by-nome?nome={nome}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var especialidade = await response.Content.ReadFromJsonAsync<EspecialidadeResponse>();
            especialidade.Should().NotBeNull();
            especialidade!.Nome.Should().Be(nome);
        }

        [Fact(DisplayName = "Deve retornar 200 OK ao buscar especialidade por categoria")]
        public async Task GetByCategoriaAsync_DeveRetornar200()
        {
            var categoria = "Clínico";

            var response = await HttpClient.GetAsync($"api/Especialidade/get-by-categoria={categoria}");

            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var especialidade = await response.Content.ReadFromJsonAsync<EspecialidadeResponse>();
            especialidade.Should().NotBeNull();
            especialidade!.Categoria.Should().Be(categoria);
        }
    }
}
 
