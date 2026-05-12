using BarberLayered.Controllers;
using BarberLayered.Models;
using BarberLayered.Tests.Helpers;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Identity;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Api;

public class AccountEndpointTests
{
    private readonly Mock<ILoginService> _loginService = new();
    private readonly Mock<IEmailSenderService> _emailSender = new();

    private static LoginViewModel ValidLoginModel() => new()
    {
        Email = "user@test.com",
        Password = "secret"
    };

    [Fact]
    [Trait("Category", "Smoke")]
    public void IndexShouldReturnView()
    {
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(), _emailSender.Object);

        var result = controller.Index();

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void Login_Get_ShouldReturnView()
    {
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(), _emailSender.Object);

        var result = controller.Login();

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task Login_Post_WhenServiceReturnsError_ShouldReturnViewWithModelAndTempData()
    {
        var session = new TestSession();
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(session), _emailSender.Object)
        {
            TempData = ControllerTestHelpers.CreateTempData()
        };
        var model = ValidLoginModel();
        _loginService
            .Setup(x => x.Login(model.Email, model.Password))
            .ReturnsAsync(new UserExtDto { ErrorMsg = "Invalid credentials" });

        var result = await controller.Login(model);

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        viewResult.Model.Should().BeSameAs(model);
        controller.TempData["ErrorMessage"].Should().Be("Invalid credentials");
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task Login_Post_WhenAdminLogsIn_ShouldRedirectToAdminHome()
    {
        var session = new TestSession();
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(session), _emailSender.Object);
        var model = ValidLoginModel();
        _loginService.Setup(x => x.Login(model.Email, model.Password)).ReturnsAsync(new UserExtDto
        {
            Id = "admin-1",
            Name = "Admin",
            Email = model.Email,
            UserType = _UserType.Admin,
            ErrorMsg = ""
        });

        var result = await controller.Login(model);

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("AdminHome");
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task LoginPostWhenBarberLogsInShouldRedirectToBarberHome()
    {
        var session = new TestSession();
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(session), _emailSender.Object);
        var model = ValidLoginModel();
        _loginService.Setup(x => x.Login(model.Email, model.Password)).ReturnsAsync(new UserExtDto
        {
            Id = "barber-1",
            Name = "Barber",
            Email = model.Email,
            UserType = _UserType.Barber,
            ErrorMsg = ""
        });

        var result = await controller.Login(model);

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("BarberHome");
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task LoginPostWhenClientLogsInShouldRedirectToClientHome()
    {
        var session = new TestSession();
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(session), _emailSender.Object);
        var model = ValidLoginModel();
        _loginService.Setup(x => x.Login(model.Email, model.Password)).ReturnsAsync(new UserExtDto
        {
            Id = "client-1",
            Name = "Client",
            Email = model.Email,
            UserType = _UserType.Client,
            ErrorMsg = ""
        });

        var result = await controller.Login(model);

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("ClientHome");
    }

    [Fact]
    public async Task LoginPostWhenSuccessfulShouldWriteUserTypeToSession()
    {
        var session = new TestSession();
        var accessor = ControllerTestHelpers.CreateHttpContextAccessor(session);
        var controller = new AccountController(_loginService.Object, accessor, _emailSender.Object);
        var model = ValidLoginModel();
        _loginService.Setup(x => x.Login(model.Email, model.Password)).ReturnsAsync(new UserExtDto
        {
            Id = "client-1",
            Name = "Client",
            Email = model.Email,
            UserType = _UserType.Client,
            ErrorMsg = ""
        });

        await controller.Login(model);

        session.TryGetValue("UserType", out var bytes).Should().BeTrue();
        BitConverter.ToInt32(bytes!, 0).Should().Be((int)_UserType.Client);
    }

    [Fact]
    public async Task LoginPostWhenSuccessfulShouldWriteIdToSession()
    {
        var session = new TestSession();
        var accessor = ControllerTestHelpers.CreateHttpContextAccessor(session);
        var controller = new AccountController(_loginService.Object, accessor, _emailSender.Object);
        var model = ValidLoginModel();
        _loginService.Setup(x => x.Login(model.Email, model.Password)).ReturnsAsync(new UserExtDto
        {
            Id = "client-42",
            Name = "Client",
            Email = model.Email,
            UserType = _UserType.Client,
            ErrorMsg = ""
        });

        await controller.Login(model);

        session.TryGetValue("Id", out var bytes).Should().BeTrue();
        System.Text.Encoding.UTF8.GetString(bytes!).Should().Be("client-42");
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void LogoutShouldRedirectToBarberShopIndex()
    {
        var controller = new AccountController(_loginService.Object, ControllerTestHelpers.CreateHttpContextAccessor(), _emailSender.Object);

        var result = controller.Logout();

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("BarberShop");
    }
}
