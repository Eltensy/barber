using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Services;

public class ServiceServiceTests
{
    private readonly Mock<IServiceRepository> _serviceRepository = new();

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task GetServicesByBarberId_ShouldMapEntities()
    {
        var service = new ServiceService(_serviceRepository.Object);
        _serviceRepository.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(new List<Service>
        {
            new() { Id = 1, fk_BarberId = "barber-1", Title = "Cut", Price = 100, Duration = new TimeOnly(1, 0) }
        });

        var result = await service.GetServicesByBarberId("barber-1");

        result.Should().HaveCount(1);
        result[0].Title.Should().Be("Cut");
    }

    [Fact]
    public async Task InsertService_ShouldMapDtoAndCallRepository()
    {
        var service = new ServiceService(_serviceRepository.Object);
        Service? captured = null;
        _serviceRepository.Setup(x => x.InsertService(It.IsAny<Service>()))
            .Callback<Service>(entity => captured = entity)
            .Returns(Task.CompletedTask);

        await service.InsertService(new ServiceDto
        {
            Id = 1,
            fk_BarberId = "barber-1",
            Title = "Cut",
            Description = "Basic cut",
            Price = 100,
            Duration = new TimeOnly(1, 0)
        });

        captured.Should().NotBeNull();
        captured!.Title.Should().Be("Cut");
        captured.fk_BarberId.Should().Be("barber-1");
    }

    [Fact]
    public async Task DeleteService_ShouldPassIdToRepository()
    {
        var service = new ServiceService(_serviceRepository.Object);

        await service.DeleteService(15);

        _serviceRepository.Verify(x => x.DeleteService(15), Times.Once);
    }
}
