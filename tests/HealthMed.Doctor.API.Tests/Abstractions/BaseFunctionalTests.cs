using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace HealthMed.Doctor.API.Tests.Abstractions
{
  public  class BaseFunctionalTests : IClassFixture<FunctionalTestWebAppFactory>
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

            using var authClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:8081")
            };

            HttpResponseMessage responseToken = null;

            for (int i = 0; i < 10; i++)
            {
                try
                {
                    responseToken = await authClient.PostAsJsonAsync("/Auth/login", usuario);
                    if (responseToken.IsSuccessStatusCode)
                    {
                        break;
                    }
                }
                catch
                {
                    // Ignorar exceções de conexão recusada
                }

                await Task.Delay(1000); // aguarda 1 segundo entre tentativas
            }

            if (responseToken == null || !responseToken.IsSuccessStatusCode)
                throw new Exception("API Auth não está disponível ou falhou ao autenticar.");

            var loginResponse = await responseToken.Content.ReadFromJsonAsync<LoginResponse>();
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse!.Token);
        }
    }
}
