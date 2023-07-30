using FluentAssertions;
using NUnit.Framework;
using Todo_App.Application.TodoLists.Queries.GetTodos;
using Todo_App.Domain.Entities;
using Todo_App.Domain.ValueObjects;

namespace Todo_App.Application.IntegrationTests.TodoLists.Queries;

using static Testing;

public class GetTodosTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnPriorityLevels()
    {
        await RunAsDefaultUserAsync();

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.PriorityLevels.Should().NotBeEmpty();
    }

    [Test]
    public async Task ShouldReturnAllListsAndItems()
    {
        await RunAsDefaultUserAsync();

        await AddAsync(new TodoList
        {
            Active = true,
            Title = "Shopping",
            Colour = Colour.Blue,
            Items =
                    {
                        new TodoItem { Title = "Apples", Done = true },
                        new TodoItem { Title = "Milk", Done = true },
                        new TodoItem { Title = "Bread", Done = true },
                        new TodoItem { Title = "Toilet paper" },
                        new TodoItem { Title = "Pasta" },
                        new TodoItem { Title = "Tissues" },
                        new TodoItem { Title = "Tuna" }
                    }
        });
        await AddAsync(new TodoList
        {
            Active = false,
            Title = "mobils",
            Colour = Colour.Yellow,
            Items =
                    {
                        new TodoItem { Title = "Apple", Done = true },
                        new TodoItem { Title = "Samsung", Done = true },
                        new TodoItem { Title = "Xioami", Done = true },
                        new TodoItem { Title = "Huawie" },
                        new TodoItem { Title = "Google" }
                    }
        });

        var query = new GetTodosQuery();

        var result = await SendAsync(query);

        result.Lists.Should().HaveCount(1);
        result.Lists.First().Items.Should().HaveCount(7);
    }

    [Test]
    public async Task ShouldAcceptAnonymousUser()
    {
        var query = new GetTodosQuery();

        var action = () => SendAsync(query);

        await action.Should().NotThrowAsync<UnauthorizedAccessException>();
    }
}
