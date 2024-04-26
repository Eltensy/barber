using System.Threading.Tasks;
using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using Moq;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task Login_ValidClientCredentials_ReturnsClientDto()
        {
            // Arrange
            var email = "tnp@gn.co";
            var password = "123123123";
            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock.Setup(x => x.GetClientByEmail(email)).ReturnsAsync(new ClientDto
            {
                Id = 1,
                Email = email,
                PasswordHash = "$2a$11$hBKejQfyAYBMw.PgqaNIYufDlw06VEdRdobqmUYa5hF.T3.IvC4jq"
            }) ;
            var barberServiceMock = new Mock<IBarberService>();
            var adminServiceMock = new Mock<IAdminService>();
            var loginService = new LoginService(clientServiceMock.Object, barberServiceMock.Object, adminServiceMock.Object);
            // Act
            var result = await loginService.Login(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("", result.ErrorMsg);
        }

        [Fact]
        public async Task Login_InvalidPassword_ReturnsErrorMessage()
        {
            // Arrange
            var email = "tnp@gn.co";
            var password = "invalidpassword";
            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock.Setup(x => x.GetClientByEmail(email)).ReturnsAsync(new ClientDto
            {
                Id = 1,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("123123123")
            });
            var barberServiceMock = new Mock<IBarberService>();
            var adminServiceMock = new Mock<IAdminService>();
            var loginService = new LoginService(clientServiceMock.Object, barberServiceMock.Object, adminServiceMock.Object);

            // Act
            var result = await loginService.Login(email, password);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Wrong password", result.ErrorMsg);
        }

        // Add more test cases as needed for different scenarios
    }
}
