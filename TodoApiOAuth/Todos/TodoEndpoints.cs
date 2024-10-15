using TodoApiOAuth.Endpoints;
using TodoApiOAuth.Todos.DTO;
using TodoApiOAuth.Todos.Service;

namespace TodoApiOAuth.Todos.Endpoints;

public class TodoEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/todos")
        .RequireAuthorization(a => a.RequireAuthenticatedUser())
        .WithTags("Todos")
        .WithOpenApi();

        group.MapGet("/", (ITodoService service) => service.GetAllTodos())
        .WithSummary("Gets all todos");

        group.MapGet("/done", (ITodoService service) => service.GetDoneTodos())
        .WithSummary("Gets all done todos");

        group.MapGet("/{id}", (ITodoService service, Guid id) => service.GetTodo(id)).WithSummary("Gets a todo by id");

        group.MapPost("/", (ITodoService service, TodoDTO todoDto) => service.CreateTodo(todoDto)).WithSummary("Creates a new todo");

        group.MapPut("/{id}", (ITodoService service, Guid id, TodoDTO todoDto) => service.UpdateTodo(id, todoDto)).WithSummary("Updates a todo");

        group.MapDelete("/{id}", (ITodoService service, Guid id) => service.DeleteTodo(id)).WithSummary("Deletes a todo");
    }

}