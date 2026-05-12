using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Services;

public class LoginServiceTests
{
    private readonly Mock<IClientService> _clientService = new();
    private readonly Mock<IBarberService> _barberService = new();
    private readonly Mock<IAdminService> _adminService = new();

    private LoginService CreateService() => new(_clientService.Object, _barberService.Object, _adminService.Object);

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task Login_WhenClientExists_ShouldReturnClientUser()
    {
        var service = CreateService();
        _clientService.Setup(x => x.GetClientByEmail("client@test.com")).ReturnsAsync(new ClientDto
        {
            Id = "client-1",
            Name = "John",
            Surname = "Doe",
            Email = "client@test.com",
            Phone = "123",
            PasswordHash = "hash"
        });

        var result = await service.Login("client@test.com", "secret");

        result.UserType.Should().Be(_UserType.Client);
        result.Id.Should().Be("client-1");
        result.ErrorMsg.Should().BeEmpty();
    }

    [Fact]
    public async Task Login_WhenClientMissingAndBarberExists_ShouldReturnBarberUser()
    {
        var service = CreateService();
        _clientService.Setup(x => x.GetClientByEmail("barber@test.com")).ReturnsAsync((ClientDto?)null);
        _barberService.Setup(x => x.GetBarberByEmail("barber@test.com")).ReturnsAsync(new BarberDto
        {
            Id = "barber-1",
            Name = "Bob",
            Surname = "Ray",
            Email = "barber@test.com",
            Phone = "123",
            PasswordHash = "hash"
        });

        var result = await service.Login("barber@test.com", "secret");

        result.UserType.Should().Be(_UserType.Barber);
        result.Id.Should().Be("barber-1");
    }

    [Fact]
    public async Task Login_WhenOnlyAdminExists_ShouldReturnAdminUser()
    {
        var service = CreateService();
        _clientService.Setup(x => x.GetClientByEmail("admin@test.com")).ReturnsAsync((ClientDto?)null);
        _barberService.Setup(x => x.GetBarberByEmail("admin@test.com")).ReturnsAsync((BarberDto?)null);
        _adminService.Setup(x => x.GetAdminByEmail("admin@test.com")).ReturnsAsync(new AdminDto
        {
            Id = "admin-1",
            Name = "Alice",
            Surname = "Root",
            Email = "admin@test.com",
            Phone = "123",
            PasswordHash = "hash"
        });

        var result = await service.Login("admin@test.com", "secret");

        result.UserType.Should().Be(_UserType.Admin);
        result.Id.Should().Be("admin-1");
    }

    [Fact]
    public async Task Login_WhenUserDoesNotExist_ShouldReturnErrorMessage()
    {
        var service = CreateService();
        _clientService.Setup(x => x.GetClientByEmail("missing@test.com")).ReturnsAsync((ClientDto?)null);
        _barberService.Setup(x => x.GetBarberByEmail("missing@test.com")).ReturnsAsync((BarberDto?)null);
        _adminService.Setup(x => x.GetAdminByEmail("missing@test.com")).ReturnsAsync((AdminDto?)null);

        var result = await service.Login("missing@test.com", "secret");

        result.ErrorMsg.Should().Contain("No user with email missing@test.com was found");
    }
}
