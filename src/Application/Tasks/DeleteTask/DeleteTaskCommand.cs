using MediatR;
namespace Application.Tasks.DeleteTask;

public record DeleteTaskCommand(Guid Id) : IRequest<Guid>;