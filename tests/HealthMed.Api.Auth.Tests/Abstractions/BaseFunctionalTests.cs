using HealthMed.Auth.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HealthMed.Api.Auth.Tests.Abstractions
{
    public class BaseFunctionalTests : IClassFixture<FunctionalTestWebAppFactory>
    {
        protected HttpClient HttpClient { get; init; }

        public BaseFunctionalTests(FunctionalTestWebAppFactory webAppFactory)
        {
            HttpClient = webAppFactory.CreateClient();
            Login().GetAwaiter().GetResult();
        }

        public async Task Login()
        {
            var usuario = new LoginRequest { Login = "CRM123456", Senha = "123456" };
            var responseToken = await HttpClient.PostAsJsonAsync($"Auth/login", usuario); // Retorna token JWT Bearer
            responseToken.EnsureSuccessStatusCode();
            var tokenContent = await responseToken.Content.ReadAsStringAsync();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenContent);
        }
    }
}
