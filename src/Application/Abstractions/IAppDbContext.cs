/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions;

public interface IAppDbContext
{
    DbSet<TaskItem> Tasks { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
