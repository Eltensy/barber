using Xunit;
using Moq;
using System.Threading.Tasks;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogicLayer.Tests.Services
{
    public class BarberHomeServiceTests
    {
        [Fact]
        public async Task GetVisitsByBarberId_ReturnsEmptyList_WhenNoVisitsFound()
        {
            // Arrange
            var visitServiceMock = new Mock<IVisitService>();
            visitServiceMock.Setup(m => m.GetVisitsByBarberId(It.IsAny<int>())).ReturnsAsync(new List<VisitDto>());

            var serviceServiceMock = new Mock<IServiceService>();
            var guestServiceMock = new Mock<IGuestService>();
            var clientServiceMock = new Mock<IClientService>();

            var barberHomeService = new BarberHomeService(visitServiceMock.Object, serviceServiceMock.Object, guestServiceMock.Object, clientServiceMock.Object);

            // Act
            var result = await barberHomeService.GetVisitsByBarberId(1);

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public async Task GetVisitsByBarberId_ReturnsVisits_WhenVisitsFound()
        {
            // Arrange
            var visitServiceMock = new Mock<IVisitService>();
            visitServiceMock.Setup(m => m.GetVisitsByBarberId(It.IsAny<int>())).ReturnsAsync(new List<VisitDto>
            {
                new VisitDto { Id = 1, fk_ServiceId = 1, fk_ClientId = 1 },
                new VisitDto { Id = 2, fk_ServiceId = 2, fk_GuestId = 1 }
            });

            var serviceServiceMock = new Mock<IServiceService>();
            serviceServiceMock.Setup(m => m.GetServiceByID(1)).ReturnsAsync(new ServiceDto { Id = 1, Title = "Haircut", Duration = new TimeOnly(0, 30) });
            serviceServiceMock.Setup(m => m.GetServiceByID(2)).ReturnsAsync(new ServiceDto { Id = 2, Title = "Shave", Duration = new TimeOnly(0, 30) });

            var clientServiceMock = new Mock<IClientService>();
            clientServiceMock.Setup(m => m.GetClientById(1)).ReturnsAsync(new ClientDto { Id = 1, Name = "John", Surname = "Doe", Phone = "+38(000)0000000", Email = "test@gmail.com", PasswordHash = "$2a$11$A/Uv6.4InMkVcjTTOc7lPuLb80jCPg428IYFOIpuwDy7jkUNs2aFi" });            // Assert
           
            
           var guestServiceMock = new Mock<IGuestService>();

            var barberHomeService = new BarberHomeService(visitServiceMock.Object, serviceServiceMock.Object, guestServiceMock.Object, clientServiceMock.Object);

            // Act
            var result = await barberHomeService.GetVisitsByBarberId(1);

            
            Assert.Equal(2, result.Count);
            Assert.Equal("John Doe", result[0].VisitorFullName);
            Assert.Equal("Haircut", result[0].ServiceTitle);
            Assert.Equal("Shave", result[1].ServiceTitle);
        }
    }
}
