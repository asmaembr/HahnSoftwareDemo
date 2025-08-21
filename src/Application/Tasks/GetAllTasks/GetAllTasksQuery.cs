/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

using MediatR;

namespace Application.Tasks.GetAllTasks;

public record GetAllTasksQuery() : IRequest<IReadOnlyList<TaskDto>>;
public record TaskDto(Guid Id, string Title, bool IsCompleted, DateTime CreatedAtUtc);
