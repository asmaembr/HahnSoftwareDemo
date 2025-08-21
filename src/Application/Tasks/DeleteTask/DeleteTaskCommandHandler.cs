/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

using Application.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.Tasks.DeleteTask;

public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Guid>
{
    private readonly IAppDbContext _ctx;
    public DeleteTaskCommandHandler(IAppDbContext ctx) => _ctx = ctx;

    public async Task<Guid> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var task = await _ctx.Tasks.FindAsync(request.Id);
        if (task is null) throw new Exception("Task not found");
        _ctx.Tasks.Remove(task);
        await _ctx.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}
