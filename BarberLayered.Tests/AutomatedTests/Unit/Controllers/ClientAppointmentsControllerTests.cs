using BarberLayered.Controllers;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Controllers;

public class ClientAppointmentsControllerTests
{
    private readonly Mock<IClientService> _clientService = new();
    private readonly Mock<IServiceService> _serviceService = new();
    private readonly Mock<IBarberService> _barberService = new();
    private readonly Mock<IVisitService> _visitService = new();

    private ClientAppointmentsController CreateController()
    {
        return new ClientAppointmentsController(
            _clientService.Object,
            _serviceService.Object,
            _barberService.Object,
            _visitService.Object);
    }

    private static ClientDto CreateClient(string id = "client-1") => new()
    {
        Id = id,
        Name = "John",
        Email = "john@test.com",
        PasswordHash = "hash",
        Phone = "123",
        Surname = "Doe"
    };

    [Fact]
    public async Task ClientAppointments_WhenClientDoesNotExist_ShouldReturnNotFound()
    {
        var controller = CreateController();
        _clientService.Setup(x => x.GetClientById("missing")).ReturnsAsync((ClientDto?)null);

        var result = await controller.ClientAppointments("missing");

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task ClientAppointments_ShouldReturnViewResult()
    {
        var controller = CreateController();
        var today = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _serviceService.Setup(x => x.GetServices()).ReturnsAsync(new List<ServiceDto>());
        _barberService.Setup(x => x.GetBarbers()).ReturnsAsync(new List<BarberDto>());
        _visitService.Setup(x => x.GetVisits()).ReturnsAsync(new List<VisitDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 10, Date = today, Time = new TimeOnly(9,0) }
        });

        var result = await controller.ClientAppointments("client-1");

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task ClientAppointments_ShouldPopulateViewBag()
    {
        var controller = CreateController();
        var client = CreateClient();
        var services = new List<ServiceDto> { new() { Id = 10, fk_BarberId = "barber-1", Title = "Cut", Price = 100, Duration = new TimeOnly(1, 0) } };
        var barbers = new List<BarberDto> { new() { Id = "barber-1", Name = "Bob", Surname = "Ray", Email = "b@test.com", PasswordHash = "hash", Phone = "111" } };
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(client);
        _serviceService.Setup(x => x.GetServices()).ReturnsAsync(services);
        _barberService.Setup(x => x.GetBarbers()).ReturnsAsync(barbers);
        _visitService.Setup(x => x.GetVisits()).ReturnsAsync(new List<VisitDto>());

        await controller.ClientAppointments("client-1");

        ((ClientDto)controller.ViewBag.Client).Id.Should().Be(client.Id);
        ((List<ServiceDto>)controller.ViewBag.Services).Should().HaveCount(1);
        ((List<BarberDto>)controller.ViewBag.Barbers).Should().HaveCount(1);
    }

    [Fact]
    public async Task ClientAppointments_ShouldGroupAppointmentsByDate()
    {
        var controller = CreateController();
        var date1 = DateOnly.FromDateTime(DateTime.Now.AddDays(2));
        var date2 = DateOnly.FromDateTime(DateTime.Now.AddDays(3));
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _serviceService.Setup(x => x.GetServices()).ReturnsAsync(new List<ServiceDto>());
        _barberService.Setup(x => x.GetBarbers()).ReturnsAsync(new List<BarberDto>());
        _visitService.Setup(x => x.GetVisits()).ReturnsAsync(new List<VisitDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 10, Date = date1, Time = new TimeOnly(9,0) },
            new() { Id = 2, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 11, Date = date1, Time = new TimeOnly(10,0) },
            new() { Id = 3, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 12, Date = date2, Time = new TimeOnly(9,0) }
        });

        var result = await controller.ClientAppointments("client-1");

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var groups = viewResult.Model.Should().BeAssignableTo<IEnumerable<IGrouping<DateOnly, VisitDto>>>().Subject.ToList();
        groups.Should().HaveCount(2);
        groups[0].Should().HaveCount(2);
    }

    [Fact]
    public async Task ClientAppointmentsHistory_ShouldReturnViewResult()
    {
        var controller = CreateController();
        var yesterday = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _serviceService.Setup(x => x.GetServices()).ReturnsAsync(new List<ServiceDto>());
        _barberService.Setup(x => x.GetBarbers()).ReturnsAsync(new List<BarberDto>());
        _visitService.Setup(x => x.GetVisits()).ReturnsAsync(new List<VisitDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 10, Date = yesterday, Time = new TimeOnly(9,0) }
        });

        var result = await controller.ClientAppointmentsHistory("client-1");

        result.Should().BeOfType<ViewResult>();
    }

    [Fact]
    public async Task ClientAppointmentsHistory_ShouldOnlyIncludePastAppointments()
    {
        var controller = CreateController();
        var yesterday = DateOnly.FromDateTime(DateTime.Now.AddDays(-1));
        var tomorrow = DateOnly.FromDateTime(DateTime.Now.AddDays(1));
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _serviceService.Setup(x => x.GetServices()).ReturnsAsync(new List<ServiceDto>());
        _barberService.Setup(x => x.GetBarbers()).ReturnsAsync(new List<BarberDto>());
        _visitService.Setup(x => x.GetVisits()).ReturnsAsync(new List<VisitDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 10, Date = yesterday, Time = new TimeOnly(9,0) },
            new() { Id = 2, fk_ClientId = "client-1", fk_BarberId = "barber-1", fk_ServiceId = 10, Date = tomorrow, Time = new TimeOnly(9,0) }
        });

        var result = await controller.ClientAppointmentsHistory("client-1");

        var viewResult = result.Should().BeOfType<ViewResult>().Subject;
        var groups = viewResult.Model.Should().BeAssignableTo<IEnumerable<IGrouping<DateOnly, VisitDto>>>().Subject.ToList();
        groups.SelectMany(x => x).Should().OnlyContain(x => x.Date <= yesterday);
    }

    [Fact]
    public async Task DeleteAppointment_ShouldRedirectToClientAppointmentsWithClientId()
    {
        var controller = CreateController();

        var result = await controller.DeleteAppointment(5, "client-1");

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("ClientAppointments");
        redirect.RouteValues!["clientId"].Should().Be("client-1");
    }
}
