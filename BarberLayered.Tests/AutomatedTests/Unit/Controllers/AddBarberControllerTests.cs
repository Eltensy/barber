using BarberLayered.Controllers;
using BarberLayered.Tests.Helpers;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Controllers;

public class AddBarberControllerTests
{
    private readonly Mock<ILoginService> _loginService = new();
    private readonly Mock<IRegisterService> _registerService = new();

    private AddBarber CreateController()
    {
        return new AddBarber(_loginService.Object, _registerService.Object, ControllerTestHelpers.CreateHttpContextAccessor())
        {
            TempData = ControllerTestHelpers.CreateTempData()
        };
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void Index_ShouldReturnView()
    {
        var controller = CreateController();

        var result = controller.Index();

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task AddNewBarber_WhenRegistrationSucceeds_ShouldSetSuccessMessageAndReturnAdminHomeView()
    {
        var controller = CreateController();
        _registerService.Setup(x => x.AdminAddBarber(It.IsAny<RegistrationDto>())).ReturnsAsync(0);

        var result = await controller.AddNewBarber("Bob", "Ray", "secret", "bob@test.com", "123");

        var view = result.Should().BeOfType<ViewResult>().Subject;
        view.ViewName.Should().Be("../AdminHome/Index");
        controller.TempData["SuccessMessage"].Should().Be("Barber successfully added.");
    }

    [Fact]
    public async Task AddNewBarber_WhenRegistrationFails_ShouldSetErrorMessageAndReturnAddBarberView()
    {
        var controller = CreateController();
        _registerService.Setup(x => x.AdminAddBarber(It.IsAny<RegistrationDto>())).ReturnsAsync(-1);

        var result = await controller.AddNewBarber("Bob", "Ray", "secret", "bob@test.com", "123");

        var view = result.Should().BeOfType<ViewResult>().Subject;
        view.ViewName.Should().Be("../AdminHome/AddBarber");
        controller.TempData["ErrorMessage"].Should().Be("Error happened(");
    }

    [Fact]
    public async Task AddNewBarber_ShouldPassExpectedDtoToService()
    {
        var controller = CreateController();
        RegistrationDto? captured = null;
        _registerService
            .Setup(x => x.AdminAddBarber(It.IsAny<RegistrationDto>()))
            .Callback<RegistrationDto>(dto => captured = dto)
            .ReturnsAsync(0);

        await controller.AddNewBarber("Bob", "Ray", "secret", "bob@test.com", "123");

        captured.Should().NotBeNull();
        captured!.Name.Should().Be("Bob");
        captured.Surname.Should().Be("Ray");
        captured.Email.Should().Be("bob@test.com");
        captured.Password.Should().Be("secret");
        captured.Phone.Should().Be("123");
    }
}
