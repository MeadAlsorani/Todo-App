using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Todo_App.Application.TodoTags.Commands;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoTags.Commands;
using static Testing;
public class CreateTodoTagTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateTodoTag()
    {
        await RunAsDefaultUserAsync();
        var command = new CreateTodoTagCommand() { Name = "Tag1" };
        var tagId = await SendAsync(command);

        var tag = await FindAsync<TodoTag>(tagId);

        Assert.IsNotNull(tag);
    }
}
