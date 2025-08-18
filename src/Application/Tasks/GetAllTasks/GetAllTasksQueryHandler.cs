using Application.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Tasks.GetAllTasks;

public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IReadOnlyList<TaskDto>>
{
    private readonly IAppDbContext _ctx;
    public GetAllTasksQueryHandler(IAppDbContext ctx) => _ctx = ctx;

    public async Task<IReadOnlyList<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        return await _ctx.Tasks
            .OrderByDescending(t => t.CreatedAtUtc)
            .Select(t => new TaskDto(t.Id, t.Title, t.IsCompleted, t.CreatedAtUtc))
            .ToListAsync(cancellationToken);
    }
}
