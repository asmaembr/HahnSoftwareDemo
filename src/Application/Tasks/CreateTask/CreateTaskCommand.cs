using MediatR;

namespace Application.Tasks.CreateTask;

public record CreateTaskCommand(string Title) : IRequest<Guid>;
