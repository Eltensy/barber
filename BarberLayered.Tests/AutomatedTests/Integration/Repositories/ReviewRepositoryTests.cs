using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberLayered.Tests.AutomatedTests.Integration.Repositories;

public class ReviewRepositoryTests
{
    [Fact]
    public async Task GetReviewsByClientId_ShouldReturnOnlyMatchingReviews()
    {
        await using var context = CreateContext();
        context.Reviews.AddRange(
            new Review { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", Rating = 4.5f, Text = "Good", Date = DateTime.UtcNow },
            new Review { Id = 2, fk_ClientId = "client-2", fk_BarberId = "barber-1", Rating = 3.5f, Text = "Ok", Date = DateTime.UtcNow });
        await context.SaveChangesAsync();
        var repository = new ReviewRepository(context);

        var result = await repository.GetReviewsByClientId("client-1");

        result.Should().HaveCount(1);
        result.Single().fk_ClientId.Should().Be("client-1");
    }

    [Fact]
    public async Task GetReviewsByBarberId_ShouldReturnOnlyMatchingReviews()
    {
        await using var context = CreateContext();
        context.Reviews.AddRange(
            new Review { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", Rating = 4.5f, Text = "Good", Date = DateTime.UtcNow },
            new Review { Id = 2, fk_ClientId = "client-2", fk_BarberId = "barber-2", Rating = 3.5f, Text = "Ok", Date = DateTime.UtcNow });
        await context.SaveChangesAsync();
        var repository = new ReviewRepository(context);

        var result = await repository.GetReviewsByBarberId("barber-1");

        result.Should().HaveCount(1);
        result.Single().fk_BarberId.Should().Be("barber-1");
    }

    [Fact]
    public async Task DeleteReview_ShouldRemoveExistingEntity()
    {
        await using var context = CreateContext();
        context.Reviews.Add(new Review { Id = 1, fk_ClientId = "client-1", fk_BarberId = "barber-1", Rating = 4.5f, Text = "Good", Date = DateTime.UtcNow });
        await context.SaveChangesAsync();
        var repository = new ReviewRepository(context);

        await repository.DeleteReview(1);

        context.Reviews.Should().BeEmpty();
    }

    private static DataContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }
}
