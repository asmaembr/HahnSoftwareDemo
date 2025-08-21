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

namespace Application.Tasks.CreateTask;

public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IAppDbContext _ctx;
    public CreateTaskCommandHandler(IAppDbContext ctx) => _ctx = ctx;

    public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        var task = new TaskItem(request.Title);
        _ctx.Tasks.Add(task);
        await _ctx.SaveChangesAsync(cancellationToken);
        return task.Id;
    }
}
