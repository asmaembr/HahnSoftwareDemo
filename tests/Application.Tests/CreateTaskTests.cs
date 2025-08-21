/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

using Application.Tasks.CreateTask;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence;
using Application.Tasks.ChangeTaskStatus;
using Application.Tasks.DeleteTask;
using Application.Tasks.GetAllTasks;
using Domain.Entities;

public class CreateTaskTests
{
    private AppDbContext GetInMemoryDbContext(string databaseName)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        return new AppDbContext(options);
    }

    [Fact]
    public async Task Should_Create_Task_With_Valid_Title()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("CreateTaskTest");
        var handler = new CreateTaskCommandHandler(ctx);
        var command = new CreateTaskCommand("Test Task");

        // Act
        var id = await handler.Handle(command, default);

        // Assert
        Assert.NotEqual(Guid.Empty, id);
        var createdTask = await ctx.Tasks.FindAsync(id);
        Assert.NotNull(createdTask);
        Assert.Equal("Test Task", createdTask.Title);
        Assert.False(createdTask.IsCompleted);
    }

    [Fact]
    public async Task Should_Change_Task_Status_When_Task_Exists()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("ChangeTaskStatusTest");
        var createHandler = new CreateTaskCommandHandler(ctx);
        var changeHandler = new ChangeTaskStatusCommandHandler(ctx);
        
        // Create a task first
        var taskId = await createHandler.Handle(new CreateTaskCommand("Test Task"), default);
        
        // Act
        var command = new ChangeTaskStatusCommand(taskId, "Test Task", true);
        var result = await changeHandler.Handle(command, default);

        // Assert
        Assert.Equal(taskId, result);
        var task = await ctx.Tasks.FindAsync(taskId);
        Assert.NotNull(task);
        Assert.True(task.IsCompleted);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Changing_Status_Of_NonExistent_Task()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("ChangeNonExistentTaskTest");
        var handler = new ChangeTaskStatusCommandHandler(ctx);
        var command = new ChangeTaskStatusCommand(Guid.NewGuid(), "Non-existent Task", true);

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_Get_All_Tasks_Ordered_By_Creation_Date()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("GetAllTasksTest");
        var createHandler = new CreateTaskCommandHandler(ctx);
        var queryHandler = new GetAllTasksQueryHandler(ctx);

        // Create some tasks
        await createHandler.Handle(new CreateTaskCommand("First Task"), default);
        await Task.Delay(10); // Small delay to ensure different timestamps
        await createHandler.Handle(new CreateTaskCommand("Second Task"), default);

        // Act
        var tasks = await queryHandler.Handle(new GetAllTasksQuery(), default);

        // Assert
        Assert.NotNull(tasks);
        Assert.Equal(2, tasks.Count);
        Assert.Equal("Second Task", tasks[0].Title); // Should be first due to OrderByDescending
        Assert.Equal("First Task", tasks[1].Title);
    }

    [Fact]
    public async Task Should_Delete_Task_When_Task_Exists()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("DeleteTaskTest");
        var createHandler = new CreateTaskCommandHandler(ctx);
        var deleteHandler = new DeleteTaskCommandHandler(ctx);
        
        // Create a task first
        var taskId = await createHandler.Handle(new CreateTaskCommand("Task to Delete"), default);

        // Act
        var result = await deleteHandler.Handle(new DeleteTaskCommand(taskId), default);

        // Assert
        Assert.Equal(taskId, result);
        var deletedTask = await ctx.Tasks.FindAsync(taskId);
        Assert.Null(deletedTask);
    }

    [Fact]
    public async Task Should_Throw_Exception_When_Deleting_NonExistent_Task()
    {
        // Arrange
        using var ctx = GetInMemoryDbContext("DeleteNonExistentTaskTest");
        var handler = new DeleteTaskCommandHandler(ctx);
        var command = new DeleteTaskCommand(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<Exception>(() => handler.Handle(command, default));
    }

    [Fact]
    public async Task Should_Toggle_Task_Completion_Status()
    {
        // First, let's test the TaskItem toggle behavior directly
        var taskItem = new TaskItem("Test Task");
        Assert.False(taskItem.IsCompleted); // Initially false
        
        taskItem.ToggleComplete();
        Assert.True(taskItem.IsCompleted); // After first toggle, true
        
        taskItem.ToggleComplete();
        Assert.False(taskItem.IsCompleted); // After second toggle, false again

        // Now test with the handlers
        using var ctx = GetInMemoryDbContext("ToggleTaskTest");
        var createHandler = new CreateTaskCommandHandler(ctx);
        var changeHandler = new ChangeTaskStatusCommandHandler(ctx);
        
        // Create a task (initially not completed)
        var taskId = await createHandler.Handle(new CreateTaskCommand("Toggle Task"), default);
        
        // First toggle
        await changeHandler.Handle(new ChangeTaskStatusCommand(taskId, "Toggle Task", true), default);
        
        ctx.ChangeTracker.Clear();
        var taskAfterFirstToggle = await ctx.Tasks.FindAsync(taskId);
        Assert.NotNull(taskAfterFirstToggle);
        Assert.True(taskAfterFirstToggle.IsCompleted);

        // Second toggle
        await changeHandler.Handle(new ChangeTaskStatusCommand(taskId, "Toggle Task", false), default);
        
        ctx.ChangeTracker.Clear();
        var taskAfterSecondToggle = await ctx.Tasks.FindAsync(taskId);
        Assert.NotNull(taskAfterSecondToggle);
        Assert.False(taskAfterSecondToggle.IsCompleted);
    }
}
