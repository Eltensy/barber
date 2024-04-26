using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BarberLayered.Models; // Assuming these are the namespaces for Client, Barber, and Admin classes
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using Moq;
using Xunit;

namespace BusinessLogicLayer.Tests.Services
{
    public class LoginServiceTests
    {
        [Fact]
        public async Task Login_Client_Successful()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            var barberServiceMock = new Mock<IBarberService>();
            var adminServiceMock = new Mock<IAdminService>();

            var clientDto = new ClientDto
            {
                Name = "John",
                Surname = "Doe",
                Phone = "123456789",
                Email = "client@example.com",
                PasswordHash = "hashedPassword" // Assuming you have the hashed password
            };

            var client = new Client(clientDto);

            clientServiceMock.Setup(m => m.GetClientByEmail("client@example.com")).ReturnsAsync(clientDto);

            var loginService = new LoginService(clientServiceMock.Object, barberServiceMock.Object, adminServiceMock.Object);

            // Act
            var result = await loginService.Login("client@example.com", "password");

            // Assert
            Assert.Equal(1, result);
        }

        [Fact]
        public async Task Login_Barber_Successful()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            var barberServiceMock = new Mock<IBarberService>();
            var adminServiceMock = new Mock<IAdminService>();

            var barberDto = new BarberDto
            {
                Name = "Andrew",
                Surname = "Skvarko",
                Phone = "+38(068)8627181",
                Email = "skvarkoandriy@gmail.com",
                PasswordHash = "$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi"
            };

            var barber = new Barber(barberDto);
            barberServiceMock.Setup(m => m.GetBarberByEmail("barber@example.com")).ReturnsAsync(barberDto);

            var loginService = new LoginService(clientServiceMock.Object, barberServiceMock.Object, adminServiceMock.Object);

            // Act
            var result = await loginService.Login("barber@example.com", "password");

            // Assert
            Assert.Equal(2, result);
        }

        [Fact]
        public async Task Login_Admin_Successful()
        {
            // Arrange
            var clientServiceMock = new Mock<IClientService>();
            var barberServiceMock = new Mock<IBarberService>();
            var adminServiceMock = new Mock<IAdminService>();

            var adminDto = new AdminDto
            {
                Name = "Admin",
                Surname = "User",
                Phone = "555555555",
                Email = "admin@example.com",
                PasswordHash = "hashedPassword" // Assuming you have the hashed password
            };

            var admin = new Admin(adminDto);
            adminServiceMock.Setup(m => m.GetAdminByEmail("admin@example.com")).ReturnsAsync(adminDto);

            var loginService = new LoginService(clientServiceMock.Object, barberServiceMock.Object, adminServiceMock.Object);

            // Act
            var result = await loginService.Login("admin@example.com", "password");

            // Assert
            Assert.Equal(3, result);
        }
    }
}
