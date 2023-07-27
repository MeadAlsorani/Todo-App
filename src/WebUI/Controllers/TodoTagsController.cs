using Microsoft.AspNetCore.Mvc;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Application.TodoTags.Commands;

namespace Todo_App.WebUI.Controllers;

public class TodoTagsController : ApiControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Creat(CreateTodoTagCommand command)
    {
        return Ok(await Mediator.Send(command));
    }
}
