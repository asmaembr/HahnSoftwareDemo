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
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class AppDbContext : DbContext, IAppDbContext
{
    public DbSet<TaskItem> Tasks => Set<TaskItem>();

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
