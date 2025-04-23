using Bogus;
using HealthMed.Api.Auth.Tests.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using HealthMed.Auth.Application.ViewModels;
using FluentAssertions;

namespace HealthMed.Api.Auth.Tests
{
   public class AuthControllerTests : BaseFunctionalTests
    {
        private readonly Faker _faker;
        private readonly FunctionalTestWebAppFactory _testsFixture;

        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthControllerTests(FunctionalTestWebAppFactory testsFixture) : base(testsFixture)
        {
            _testsFixture = testsFixture;
            _faker = new Faker(locale: "pt_BR");
        }
        [Fact(DisplayName = "Deve gerar Token Válido")]
        [Trait("Functional", "TokenController")]
        public async Task Should_Return_TokenCreatedWithSuccess()
        {
            //Arrange
            var usuario = new LoginRequest { Login = "CRM123456", Senha = "123456" };

            //Act
            var response = await HttpClient.PostAsJsonAsync($"Auth/login", usuario);

            //Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact(DisplayName = "Deve gerar erro de não autorizado")]
        [Trait("Functional", "TokenController")]
        public async Task Should_Return_ErrorTokenUnauthorized()
        {
            //Arrange
            var usuario = new LoginRequest { Login = "CRM123456", Senha = "admin@123" };

            //Act
            var response = await HttpClient.PostAsJsonAsync($"Auth/login", usuario);

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        }
    }
}
