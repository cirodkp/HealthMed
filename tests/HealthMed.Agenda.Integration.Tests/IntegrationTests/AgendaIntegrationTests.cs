using Bogus;
using FluentAssertions;
using HealthMed.Auth.Application.ViewModels;
using HealthMed.Agenda.Application.ViewModels;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using FluentAssertions.Extensions;
using HealthMed.Agenda.Integration.Tests.Extensions;

namespace HealthMed.Agenda.Integration.Tests.IntegrationTests
{
    public class AgendaIntegrationTests
    {
        private readonly Faker _faker;
        private readonly HttpClient _client;
        private readonly string _authUrl = "http://localhost:8081";
        private readonly string _apiAgendaUrl = "http://localhost:8085";

        public AgendaIntegrationTests()
        {
            _client = new HttpClient();
            _faker = new Faker(locale: "pt_BR");
        }

        [Fact]
        public async Task IntegrationAgenda_ShouldInsertAndGetInsertedFromDatabase()
        {
            // Autenticate
            await this.Login();

            // Insert Agenda In Queue
            var randomDateBetweenNextSevenDays = _faker.Date.Between(DateTime.Now.AddDays(1), DateTime.Now.AddDays(7)).AsUtc();
            var AgendaRequest = new CadastrarHorarioRequest(1, randomDateBetweenNextSevenDays);

            var cadastrarResponse = await _client.PostAsJsonAsync($"{_apiAgendaUrl}/api/agenda/horarios", AgendaRequest);
            cadastrarResponse.EnsureSuccessStatusCode();

            var publishedResponse = await cadastrarResponse.Content.ReadFromJsonAsync<PublishResponse>();
            publishedResponse.Should().NotBeNull();
            publishedResponse.Message.Should().Be("Cadastro em processamento.");
            publishedResponse.Data.Should().NotBeNull();

            var publishedResponseData = JsonConvert.DeserializeObject<HorarioDisponivelResponse>(publishedResponse.Data.ToString());
            publishedResponseData!.Should().BeEquivalentTo(AgendaRequest);

            // Aguarda até que o horário esteja persistido no banco
            await WaitForAgendaToBePersistedAsync(
                () => _client.GetFromJsonAsync<List<HorarioDisponivelResponse>>(
                    $"{_apiAgendaUrl}/api/agenda/medico/{AgendaRequest.MedicoId}"),
                publishedResponseData.DataHora,
                TimeSpan.FromMinutes(1),
                TimeSpan.FromSeconds(1));
        }

        private async Task WaitForAgendaToBePersistedAsync(
            Func<Task<List<HorarioDisponivelResponse>?>> fetchFunc,
            DateTime esperado,
            TimeSpan tolerancia,
            TimeSpan pollingInterval)
        {
            var timeout = DateTime.UtcNow.AddSeconds(60);

            while (DateTime.UtcNow < timeout)
            {
                var lista = await fetchFunc();
                if (lista != null && lista.Select(x => x.DataHora).Any(data =>
                    data.ToUniversalTime() >= esperado.ToUniversalTime() - tolerancia &&
                    data.ToUniversalTime() <= esperado.ToUniversalTime() + tolerancia))
                {
                    return; // Sucesso
                }

                await Task.Delay(pollingInterval);
            }

            throw new Exception($"Agenda não persistida dentro da tolerância de {tolerancia.TotalSeconds} segundos a partir de {esperado.ToUniversalTime()}.");
        }

        public async Task Login(string login = "CRMADMIN", string senha = "123456")
        {
            var loginRequest = new LoginRequest { Login = login, Senha = senha };
            var responseToken = await _client.PostAsJsonAsync($"{_authUrl}/auth/login", loginRequest);
            responseToken.EnsureSuccessStatusCode();
            var loginResponse = await responseToken.Content.ReadFromJsonAsync<LoginResponse>();
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResponse!.Token);
        }
    }
}
