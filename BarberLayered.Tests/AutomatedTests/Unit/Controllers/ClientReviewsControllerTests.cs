using BarberLayered.Controllers;
using BarberLayered.Models;
using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services.Interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace BarberLayered.Tests.AutomatedTests.Unit.Controllers;

public class ClientReviewsControllerTests
{
    private readonly Mock<IClientService> _clientService = new();
    private readonly Mock<IReviewService> _reviewService = new();

    private ClientReviewsController CreateController() => new(_clientService.Object, _reviewService.Object);

    private static ClientDto CreateClient() => new()
    {
        Id = "client-1",
        Name = "John",
        Surname = "Doe",
        Email = "john@test.com",
        PasswordHash = "hash",
        Phone = "123"
    };

    [Fact]
    public async Task ClientReviews_WhenClientDoesNotExist_ShouldReturnNotFound()
    {
        var controller = CreateController();
        _clientService.Setup(x => x.GetClientById("missing")).ReturnsAsync((ClientDto?)null);

        var result = await controller.ClientReviews("missing");

        result.Should().BeOfType<NotFoundResult>();
    }

    [Fact]
    [Trait("Category", "Smoke")]
    public async Task ClientReviews_ShouldReturnViewWithReviewList()
    {
        var controller = CreateController();
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _reviewService.Setup(x => x.GetReviewsByClientId("client-1")).ReturnsAsync(new List<ReviewDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", Rating = 4.5f, Text = "Good", Date = DateTime.UtcNow }
        });

        var result = await controller.ClientReviews("client-1");

        var view = result.Should().BeOfType<ViewResult>().Subject;
        var model = view.Model.Should().BeAssignableTo<List<Review>>().Subject;
        model.Should().HaveCount(1);
    }

    [Fact]
    public async Task ClientReviews_ShouldPopulateClientInViewBag()
    {
        var controller = CreateController();
        var client = CreateClient();
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(client);
        _reviewService.Setup(x => x.GetReviewsByClientId("client-1")).ReturnsAsync(new List<ReviewDto>());

        await controller.ClientReviews("client-1");

        ((ClientDto)controller.ViewBag.Client).Id.Should().Be(client.Id);
    }

    [Fact]
    public async Task ClientReviews_ShouldPopulateReviewsInViewBag()
    {
        var controller = CreateController();
        _clientService.Setup(x => x.GetClientById("client-1")).ReturnsAsync(CreateClient());
        _reviewService.Setup(x => x.GetReviewsByClientId("client-1")).ReturnsAsync(new List<ReviewDto>
        {
            new() { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", Rating = 4.5f, Text = "Good", Date = DateTime.UtcNow }
        });

        await controller.ClientReviews("client-1");

        ((List<Review>)controller.ViewBag.Reviews).Should().HaveCount(1);
    }

    [Fact]
    public async Task DeleteReview_ShouldRedirectToClientReviewsWithClientId()
    {
        var controller = CreateController();

        var result = await controller.DeleteReview(10, "client-1");

        var redirect = result.Should().BeOfType<RedirectToActionResult>().Subject;
        redirect.ActionName.Should().Be("ClientReviews");
        redirect.RouteValues!["clientId"].Should().Be("client-1");
    }
}
