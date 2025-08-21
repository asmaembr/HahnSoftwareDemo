using MediatR;
namespace Application.Tasks.ChangeTaskStatus;

public record ChangeTaskStatusCommand(Guid Id, string Title, bool IsCompleted) : IRequest<Guid>;