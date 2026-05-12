using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberLayered.Tests.AutomatedTests.Integration.Repositories;

public class ServiceRepositoryTests
{
    [Fact]
    public async Task InsertService_ShouldPersistEntity()
    {
        await using var context = CreateContext();
        var repository = new ServiceRepository(context);

        await repository.InsertService(new Service
        {
            Id = 1,
            fk_BarberId = "barber-1",
            Title = "Cut",
            Description = "Basic cut",
            Price = 100,
            Duration = new TimeOnly(1, 0)
        });

        context.Services.Should().HaveCount(1);
    }

    [Fact]
    public async Task GetServicesByBarberId_ShouldReturnOnlyMatchingServices()
    {
        await using var context = CreateContext();
        context.Services.AddRange(
            new Service { Id = 1, fk_BarberId = "barber-1", Title = "Cut", Price = 100, Duration = new TimeOnly(1, 0) },
            new Service { Id = 2, fk_BarberId = "barber-2", Title = "Shave", Price = 80, Duration = new TimeOnly(0, 30) });
        await context.SaveChangesAsync();
        var repository = new ServiceRepository(context);

        var result = await repository.GetServicesByBarberId("barber-1");

        result.Should().HaveCount(1);
        result.Single().Id.Should().Be(1);
    }

    private static DataContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }
}
