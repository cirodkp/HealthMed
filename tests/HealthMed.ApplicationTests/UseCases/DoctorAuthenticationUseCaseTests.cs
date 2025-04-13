using HealthMed.Application.Commands;
using HealthMed.Application.Events;
using HealthMed.Application.Interfaces;
using HealthMed.Application.Results;
using HealthMed.Application.Services;
using HealthMed.ApplicationTests.Configuration;
using Microsoft.Extensions.Configuration;
using Moq;

namespace HealthMed.ApplicationTests.UseCases
{
    public class DoctorAuthenticationUseCaseTests
    {
        private readonly IConfiguration _configuration;
        private readonly Mock<IDoctorPublisher> _doctorPublisherMock;
        private readonly AuthenticationDoctorServices _authenticationUseCase;
        public DoctorAuthenticationUseCaseTests()
        {
            _configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(ConfigurationManagerProperties.GetConfigurationProperties())
            .Build();

            _doctorPublisherMock = new Mock<IDoctorPublisher>();

            _authenticationUseCase = new AuthenticationDoctorServices(_configuration, _doctorPublisherMock.Object);
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

            _doctorPublisherMock
                .Setup(p => p.RequestLoginDoctorSync(It.IsAny<DoctorLoginEvent>()))
                .ReturnsAsync(new DoctorLoginEventResponse
                {
                    IsAuthenticated = false,
                    ErrorMessage = expectedErrorMessage
                });

            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _authenticationUseCase.Execute(command));

            Assert.Equal(expectedErrorMessage, exception.Message);
        }

        [Theory(DisplayName = "Deve obter token com sucesso se credenciais estão corretas.")]
        [InlineData("admin", "admin@123")] // Credenciais preenchidas e válidas
        public async Task Authenticate_WhenCrmOrPasswordIsCorrect_ShouldReturnToken(string crm, string password)
        {
            var command = new DoctorAuthenticationCommand { Crm = crm, Password = password };

            _doctorPublisherMock
                .Setup(p => p.RequestLoginDoctorSync(It.IsAny<DoctorLoginEvent>()))
                .ReturnsAsync(new DoctorLoginEventResponse
                {
                    IsAuthenticated = true,
                    Name = "Dr. Teste OK"
                });

            Assert.IsType<DoctorCredentialsResponse>(await _authenticationUseCase.Execute(command));
        }
    }
}
