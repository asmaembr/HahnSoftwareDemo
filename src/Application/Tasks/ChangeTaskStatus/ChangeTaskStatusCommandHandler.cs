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
using MediatR;

namespace Application.Tasks.ChangeTaskStatus;
public class ChangeTaskStatusCommandHandler : IRequestHandler<ChangeTaskStatusCommand, Guid>
{
    private readonly IAppDbContext _ctx;
    public ChangeTaskStatusCommandHandler(IAppDbContext ctx) => _ctx = ctx;

    public async Task<Guid> Handle(ChangeTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var task = await _ctx.Tasks.FindAsync(request.Id);
        if (task is null) throw new Exception("Task not found");
        task.ToggleComplete();
        _ctx.Tasks.Update(task);
        await _ctx.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}

