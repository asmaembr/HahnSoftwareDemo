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
using Application.Tasks.GetAllTasks;
using Application.Tasks.ChangeTaskStatus;
using Application.Tasks.DeleteTask;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly IMediator _mediator;
    public TasksController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<TaskDto>> Get() =>
        await _mediator.Send(new GetAllTasksQuery());

    [HttpPost]
    public async Task<IActionResult> Create(CreateTaskCommand cmd)
    {
        var id = await _mediator.Send(cmd);
        return Ok(id);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> ToggleComplete(Guid id, ChangeTaskStatusCommand cmd)
    {
        if (id != cmd.Id) return BadRequest();
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, DeleteTaskCommand cmd)
    {
        if (id != cmd.Id) return BadRequest();
        var result = await _mediator.Send(cmd);
        return Ok(result);
    }
}
