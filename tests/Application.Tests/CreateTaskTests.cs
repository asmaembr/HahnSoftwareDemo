using Application.Tasks.CreateTask;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;

public class CreateTaskTests
{
    [Fact]
    public async Task Should_Create_Task()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase("TestDb").Options;
        var ctx = new AppDbContext(options);
        var handler = new CreateTaskCommandHandler(ctx);
        var id = await handler.Handle(new CreateTaskCommand("Test task"), default);
        Assert.NotEqual(Guid.Empty, id);
    }
}
