using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Todo_App.Application.Common.Interfaces;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.TodoTags.Commands;
public record CreateTodoTagCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}

public class CreateTodoTagCommandHandler : IRequestHandler<CreateTodoTagCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoTagCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<int> Handle(CreateTodoTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new TodoTag
        {
            Name = request.Name
        };
        await _context.TodoTags.AddAsync(tag);
        await _context.SaveChangesAsync(cancellationToken);
        return tag.Id;
    }
}
