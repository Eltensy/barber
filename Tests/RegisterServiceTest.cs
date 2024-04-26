using Xunit;
using Moq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Services.Implementations;

namespace BusinessLogicLayer.Tests.Services
{
    public class RegisterServiceTests
    {
        [Fact]
        public async Task Register_NewClient_Success()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock.Setup(m => m.GetClientByEmail(It.IsAny<string>())).ReturnsAsync((ClientDto)null);

            var registerService = new RegisterService(clientServiceMock.Object, null, null);

            var clientDto = new ClientDto
            {
                Name = "John",
                Surname = "Doe",
                Phone = "123456789",
                Email = "client@example.com",
                PasswordHash = "password123" // Assuming you have the plaintext password
            };

            // Act
            var result = await registerService.Register(clientDto);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task BarberRegister_NewBarber_Success()
        {
            // Arrange
            var barberServiceMock = new Mock<IBarberService>();
            barberServiceMock.Setup(m => m.GetBarberByEmail(It.IsAny<string>())).ReturnsAsync((BarberDto)null);

            var registerService = new RegisterService(null, barberServiceMock.Object, null);

            var barberDto = new BarberDto
            {
                Name = "Jane",
                Surname = "Smith",
                Phone = "987654321",
                Email = "barber@example.com",
                PasswordHash = "password123" // Assuming you have the plaintext password
            };

            // Act
            var result = await registerService.BarberRegister(barberDto);

            // Assert
            Assert.Equal(0, result);
        }

        [Fact]
        public async Task AdminRegister_NewAdmin_Success()
        {
            // Arrange
            var adminServiceMock = new Mock<IAdminService>();
            adminServiceMock.Setup(m => m.GetAdminByEmail(It.IsAny<string>())).ReturnsAsync((AdminDto)null);

            var registerService = new RegisterService(null, null, adminServiceMock.Object);

            var adminDto = new AdminDto
            {
                Name = "Admin",
                Surname = "User",
                Phone = "555555555",
                Email = "admin@example.com",
                PasswordHash = "password123" // Assuming you have the plaintext password
            };

            // Act
            var result = await registerService.AdminRegister(adminDto);

            // Assert
            Assert.Equal(0, result);
        }
    }
}
