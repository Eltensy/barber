using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using Moq;
using Serilog;
using System.Net;
using Xunit;

namespace BusinessLogicLayer.Tests
{
    public class RegisterServiceTests
    {

        [Fact]
        public async Task Register_Client_Success()
        {
            var adminServiceMock = new Mock<IAdminService>();
            var barberServiceMock = new Mock<IBarberService>();
            var clientServiceMock = new Mock<IClientService>();
            var registrationKeyServiceMock = new Mock<IRegistrationKeyService>();

            var registerService = new RegisterService(adminServiceMock.Object, barberServiceMock.Object, clientServiceMock.Object, registrationKeyServiceMock.Object);

            var registrationDto = new RegistrationDto
            {
                Email = "test@example.com",
                Password = "password",
                UserType = (_UserType)UserType.Client
            };

            clientServiceMock.Setup(x => x.GetClientByEmail(It.IsAny<string>())).ReturnsAsync((ClientDto)null);
            clientServiceMock.Setup(x => x.InsertClient(It.IsAny<ClientDto>())).Returns(Task.CompletedTask);

            // Act
            var result = await registerService.Register(registrationDto);

            // Assert
            Assert.Equal("", result.ErrorMsg);
            Assert.IsType<UserExtDto>(result);
            Assert.Equal("test@example.com", result.Email);
        }

        [Fact]
        public async Task Register_Barber_Success()
        {
            var adminServiceMock = new Mock<IAdminService>();
            var barberServiceMock = new Mock<IBarberService>();
            var clientServiceMock = new Mock<IClientService>();
            var registrationKeyServiceMock = new Mock<IRegistrationKeyService>();

            var registerService = new RegisterService(adminServiceMock.Object, barberServiceMock.Object, clientServiceMock.Object, registrationKeyServiceMock.Object);
            var registrationDto = new RegistrationDto
            {
                Email = "test@example.com",
                Password = "password",
                UserType = (_UserType)UserType.Barber,
                RegistrationKey = "qwerty123"
            };
            barberServiceMock.Setup(x => x.GetBarberByEmail(It.IsAny<string>())).ReturnsAsync((BarberDto)null);
            barberServiceMock.Setup(x => x.InsertBarber(It.IsAny<BarberDto>())).Returns(Task.CompletedTask);
            // Act
            registrationKeyServiceMock.Setup(x => x.GetRegistrationKeyFirst()).ReturnsAsync(new RegistrationKeyDto { Key = "valid_key" });

            // Act
            var result = await registerService.Register(registrationDto);

            // Assert
            Assert.Null(result.ErrorMsg);
            Assert.IsType<UserExtDto>(result);
            Assert.Equal("test@example.com", result.Email);
            // Add more assertions as needed
        }

        [Fact]
        public async Task Register_Admin_Success()
        {
            // Arrange
            var adminServiceMock = new Mock<IAdminService>();
            var barberServiceMock = new Mock<IBarberService>();
            var clientServiceMock = new Mock<IClientService>();
            var registrationKeyServiceMock = new Mock<IRegistrationKeyService>();

            var registerService = new RegisterService(adminServiceMock.Object, barberServiceMock.Object, clientServiceMock.Object, registrationKeyServiceMock.Object);
            var registrationDto = new RegistrationDto
            {
                Email = "test@example.com",
                Password = "password",
                UserType = (_UserType)UserType.Admin,
                RegistrationKey = "qwerty123"
            };
            adminServiceMock.Setup(x => x.GetAdminByEmail(It.IsAny<string>())).ReturnsAsync((AdminDto)null);
            adminServiceMock.Setup(x => x.InsertAdmin(It.IsAny<AdminDto>())).Returns(Task.CompletedTask);
            // Act
            registrationKeyServiceMock.Setup(x => x.GetRegistrationKeyFirst()).ReturnsAsync(new RegistrationKeyDto { Key = "valid_key" });

            // Act
            var result = await registerService.Register(registrationDto);

            // Assert
            Assert.Null(result.ErrorMsg);
            Assert.IsType<UserExtDto>(result);
            Assert.Equal("test@example.com", result.Email);
        }
    }


}
