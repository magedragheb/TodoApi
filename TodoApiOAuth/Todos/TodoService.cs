using TodoApiOAuth.Todos.DTO;
using TodoApiOAuth.Todos.Entity;
using TodoApiOAuth.Todos.Repo;

namespace TodoApiOAuth.Todos.Service;

public class TodoService(
    ITodoRepo repo, 
    IHttpContextAccessor context) : ITodoService
{
    public async Task<IResult> GetAllTodos() => TypedResults.Ok(await repo.GetAllTodos());

    public async Task<IResult> GetDoneTodos() => TypedResults.Ok(await repo.GetDoneTodos());

    public async Task<IResult> GetTodo(Guid id)
    {
        var todo = await repo.GetTodo(id);
        return todo is null ? TypedResults.NotFound() : TypedResults.Ok(todo);
    }

    public async Task<IResult> CreateTodo(TodoDTO todoDto)
    {
        var todo = new Todo{ Title = todoDto.Title , IsDone = todoDto.IsDone };
        if (context.HttpContext is null) return TypedResults.BadRequest();
        var claim = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "Id");
        if (claim is null) return TypedResults.Unauthorized();
        todo.UserId = new Guid(claim.Value);
        await repo.CreateTodo(todo);
        return TypedResults.Created($"/todos/{todo.Id}", todoDto);
    }

    public async Task<IResult> UpdateTodo(Guid id, TodoDTO input)
    {
        var todo = new Todo { Title = input.Title, IsDone = input.IsDone };
        var result = await repo.UpdateTodo(id, todo);
        return result is null ? TypedResults.NotFound() : TypedResults.NoContent();
    }

    public async Task<IResult> DeleteTodo(Guid id)
    {
        var result = await repo.DeleteTodo(id);
        return result > 0 ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}