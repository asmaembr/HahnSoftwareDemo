using Application.Tasks.CreateTask;
using Application.Tasks.GetAllTasks;
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
}
