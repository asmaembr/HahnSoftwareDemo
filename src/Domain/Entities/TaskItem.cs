/*
 * Copyright (c) 2025 Asmae Moubarriz
 * All rights reserved.
 * 
 * This is a technical assignment submission to Hahn Software.
 * 
 * Author: Asmae Moubarriz
 * Created: August 2025
 */

namespace Domain.Entities;

public class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

    public TaskItem(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required");
        Title = title.Trim();
    }

    public void ToggleComplete() => IsCompleted = !IsCompleted;
}
