﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Todo_App.Application.Common.Exceptions;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;
using Todo_App.Domain.Enums;

namespace Todo_App.Application.TodoItems.Commands.UpdateTodoItemDetail;

public record UpdateTodoItemDetailCommand : IRequest
{
    public int Id { get; init; }

    public int ListId { get; init; }

    public PriorityLevel Priority { get; init; }

    public string? Note { get; init; }
    public List<int> Tags { get; init; } = new();
}

public class UpdateTodoItemDetailCommandHandler : IRequestHandler<UpdateTodoItemDetailCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateTodoItemDetailCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateTodoItemDetailCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.TodoItems
            .Include(x => x.ItemTags)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(TodoItem), request.Id);
        }

        entity.ListId = request.ListId;
        entity.Priority = request.Priority;
        entity.Note = request.Note;
        foreach (var tag in request.Tags)
        {
            var isTagExist = entity.ItemTags.Any(t => t.TodoTagId == tag);
            if (!isTagExist)
            {
                entity.ItemTags.Add(new TodoItemTags { TodoItemId = request.Id, TodoTagId = tag });
            }
        }
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
