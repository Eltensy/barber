using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using DataAccessLayer.Repositories.Implementations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace BarberLayered.Tests.AutomatedTests.Integration.Repositories;

public class VisitRepositoryTests
{
    [Fact]
    public async Task UpdateVisit_ShouldPersistChanges()
    {
        await using var context = CreateContext();
        context.Visits.Add(new Visit
        {
            Id = 1,
            fk_ClientId = "client-1",
            fk_BarberId = "barber-1",
            fk_ServiceId = 10,
            Date = new DateOnly(2026, 1, 1),
            Time = new TimeOnly(9, 0)
        });
        await context.SaveChangesAsync();
        var repository = new VisitRepository(context);

        await repository.UpdateVisit(new Visit
        {
            Id = 1,
            fk_ClientId = "client-1",
            fk_BarberId = "barber-1",
            fk_ServiceId = 99,
            Date = new DateOnly(2026, 1, 2),
            Time = new TimeOnly(10, 0)
        });

        var updated = await context.Visits.FindAsync(1);
        updated.Should().NotBeNull();
        updated!.fk_ServiceId.Should().Be(99);
        updated.Date.Should().Be(new DateOnly(2026, 1, 2));
    }

    private static DataContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }
}
