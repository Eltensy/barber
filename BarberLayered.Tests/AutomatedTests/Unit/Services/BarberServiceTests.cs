using BusinessLogicLayer.Services.Implementations;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Interfaces;
using FluentAssertions;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Services;

public class BarberServiceTests
{
    private readonly Mock<IBarberRepository> _barberRepository = new();

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task GetBarbers_ShouldMapEntitiesToDtos()
    {
        var service = new BarberService(_barberRepository.Object);
        _barberRepository.Setup(x => x.GetBarbers()).ReturnsAsync(new List<Barber>
        {
            new() { Id = "b1", Name = "Bob", Surname = "Ray", Email = "b1@test.com", Phone = "111", PasswordHash = "hash" },
            new() { Id = "b2", Name = "Sam", Surname = "Lee", Email = "b2@test.com", Phone = "222", PasswordHash = "hash" }
        });

        var result = await service.GetBarbers();

        result.Should().HaveCount(2);
        result[0].Id.Should().Be("b1");
    }

    [Fact]
    public async Task GetBarberById_WhenRepositoryReturnsNull_ShouldReturnNull()
    {
        var service = new BarberService(_barberRepository.Object);
        _barberRepository.Setup(x => x.GetBarberByID("missing")).ReturnsAsync((Barber?)null);

        var result = await service.GetBarberById("missing");

        result.Should().BeNull();
    }

    [Fact]
    public async Task GetBarberByEmail_ShouldMapEntityToDto()
    {
        var service = new BarberService(_barberRepository.Object);
        _barberRepository.Setup(x => x.GetBarberByEmail("bob@test.com")).ReturnsAsync(new Barber
        {
            Id = "b1",
            Name = "Bob",
            Surname = "Ray",
            Email = "bob@test.com",
            Phone = "111",
            PasswordHash = "hash"
        });

        var result = await service.GetBarberByEmail("bob@test.com");

        result.Should().NotBeNull();
        result!.Email.Should().Be("bob@test.com");
    }
}
