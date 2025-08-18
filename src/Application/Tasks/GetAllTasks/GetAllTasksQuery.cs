using MediatR;

namespace Application.Tasks.GetAllTasks;

public record GetAllTasksQuery() : IRequest<IReadOnlyList<TaskDto>>;
public record TaskDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedAtUtc);
