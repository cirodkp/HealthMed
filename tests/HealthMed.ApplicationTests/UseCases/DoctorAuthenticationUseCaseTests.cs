using HealthAndMed.Application.Commands;
using HealthAndMed.Application.UseCases;
using HealthMed.Application.ViewModels;
using HealthMed.ApplicationTests.Configuration;
using HealthMed.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Moq;

namespace HealthMed.ApplicationTests.UseCases
{
    public class DoctorAuthenticationUseCaseTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<IDoctorCredentialsRepository> _doctorCredentialsRepository;
        private readonly AuthenticationUseCase _authenticationUseCase;
        public DoctorAuthenticationUseCaseTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(ConfigurationManagerProperties.GetConfigurationProperties())
                .Build();

            _doctorCredentialsRepository = new Mock<IDoctorCredentialsRepository>();
            _authenticationUseCase = new AuthenticationUseCase(_configuration, _doctorCredentialsRepository.Object);
        }

        [Theory(DisplayName = "Deve gerar uma exceção se crm ou password estiver nulo/vazio.")]
        [InlineData("", "", "CRM/Senha nulo ou vazio.")] // Ambos vazios
        [InlineData("", "123", "CRM/Senha nulo ou vazio.")] // Crm vazio
        [InlineData(null, "123", "CRM/Senha nulo ou vazio.")] // Crm nulo
        [InlineData("123456", "", "CRM/Senha nulo ou vazio.")] // Password vazio
        [InlineData("123456", null, "CRM/Senha nulo ou vazio.")] // Password nulo
        public async Task Authenticate_WhenCrmOrPasswordIsNullOrEmpty_ShouldReturnException(string crm, string password, string expectedErrorMessage)
        {
            var command = new DoctorAuthenticationCommand { Crm = crm, Password = password };
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authenticationUseCase.Execute(command));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Theory(DisplayName = "Deve gerar uma exceção se crm ou password estiver preenchido, mas inválido.")]
        [InlineData("123456789", "123456789", "Credenciais inválidas.")] // Credenciais preenchidas, mas inválidas
        public async Task Authenticate_WhenCrmOrPasswordIsIncorrect_ShouldReturnException(string crm, string password, string expectedErrorMessage)
        {
            var command = new DoctorAuthenticationCommand { Crm = crm, Password = password };
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authenticationUseCase.Execute(command));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Theory(DisplayName = "Deve obter token com sucesso se credenciais estão corretas.")]
        [InlineData("admin", "admin@123")] // Credenciais preenchidas e válidas
        public async Task Authenticate_WhenCrmOrPasswordIsCorrect_ShouldReturnToken(string crm, string password)
        {
            var command = new DoctorAuthenticationCommand { Crm = crm, Password = password };
            Assert.IsType<DoctorCredentialsResponse>(await _authenticationUseCase.Execute(command));
        }
    }
}
