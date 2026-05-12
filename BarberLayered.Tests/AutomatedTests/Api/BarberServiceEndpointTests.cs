using BarberLayered.Controllers;
using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Api;

public class BarberServiceEndpointTests
{
    private readonly Mock<IServiceService> _serviceService = new();
    private readonly Mock<IBarberService> _barberService = new();

    private BarberServiceController CreateController() => new(_serviceService.Object, _barberService.Object);

    private static BarberDto CreateBarber(string id = "barber-1") => new()
    {
        Id = id,
        Name = "Bob",
        Surname = "Ray",
        Email = "bob@test.com",
        Phone = "123",
        PasswordHash = "hash"
    };

    private static List<ServiceDto> CreateServices(string barberId = "barber-1") => new()
    {
        new() { Id = 1, fk_BarberId = barberId, Title = "Cut", Price = 100, Duration = new TimeOnly(1, 0) },
        new() { Id = 2, fk_BarberId = barberId, Title = "Shave", Price = 80, Duration = new TimeOnly(0, 30) }
    };

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task Index_ShouldReturnView()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        var result = await controller.Index("barber-1");

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task Index_ShouldPopulateBarberInViewBag()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        await controller.Index("barber-1");

        ((Barber)controller.ViewBag.Barber).Id.Should().Be("barber-1");
    }

    [Fact]
    public async Task Index_ShouldPopulateServicesInViewBag()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        await controller.Index("barber-1");

        ((List<Service>)controller.ViewBag.Services).Should().HaveCount(2);
    }

    [Fact]
    public async Task Index_ShouldSetPathToIndex()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        await controller.Index("barber-1");

        controller.ViewData["Path"].Should().Be("Index");
    }

    [Fact]
    public async Task BarberServiceClient_ShouldSetPathToServiceAppointmentClient()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        var result = await controller.BarberServiceClient("barber-1");

        result.Should().BeOfType<ViewResult>();
        controller.ViewData["Path"].Should().Be("ServiceAppointmentClient");
    }

    [Fact]
    public async Task Add_ShouldReturnView()
    {
        var controller = CreateController();
        _barberService.Setup(x => x.GetBarberById("barber-1")).ReturnsAsync(CreateBarber());
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        var result = await controller.Add("barber-1");

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task GetServices_ShouldReturnJsonResult()
    {
        var controller = CreateController();
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        var result = await controller.GetServices("barber-1");

        result.Should().BeOfType<JsonResult>();
    }

    [Fact]
    public async Task GetServices_ShouldReturnMappedServices()
    {
        var controller = CreateController();
        _serviceService.Setup(x => x.GetServicesByBarberId("barber-1")).ReturnsAsync(CreateServices());

        var result = await controller.GetServices("barber-1");

        var json = result.Should().BeOfType<JsonResult>().Subject;
        json.Value.Should().NotBeNull();
        var property = json.Value!.GetType().GetProperty("barberServices");
        property.Should().NotBeNull();
        var services = property!.GetValue(json.Value) as IEnumerable<Service>;
        services.Should().NotBeNull();
        services!.Should().HaveCount(2);
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public void SubmitAppointment_ShouldRedirectToHomeIndex()
    {
        var controller = CreateController();
        var model = new AppointmentViewModel
        {
            Name = "John",
            Phone = "123",
            SelectedBarberId = 1,
            SelectedServiceId = 2,
            SelectedDay = "Monday",
            SelectedTime = "10:00"
        };

        var result = controller.SubmitAppointment(model);

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("Index");
        redirect.ControllerName.Should().Be("Home");
    }
}
