using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.TodoItems.Queries.GetTodoItemsWithPagination;
using Todo_App.Application.TodoLists.Commands.CreateTodoList;
using Todo_App.Domain.Entities;

namespace Todo_App.Application.IntegrationTests.TodoItems.Queries;
using static Testing;
public class GetTodoItemsWithPaginationTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnActiveItems()
    {
        await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await AddAsync(new TodoItem
        {
            ListId = listId,
            Title = "Item 1",
            Priority = Domain.Enums.PriorityLevel.Low
        });
        await AddAsync(new TodoItem
        {
            ListId = listId,
            Title = "Item 2",
            Priority = Domain.Enums.PriorityLevel.High
        });
        await AddAsync(new TodoItem
        {
            ListId = listId,
            Title = "Item 3",
            Priority = Domain.Enums.PriorityLevel.Medium
        });

        var query = new GetTodoItemsWithPaginationQuery() { PageSize = 20, PageNumber = 1, ListId = listId };
        var result = await SendAsync(query);
        result.Items.Should().HaveCount(2);
    }
}
