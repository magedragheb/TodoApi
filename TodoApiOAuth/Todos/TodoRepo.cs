using Microsoft.EntityFrameworkCore;
using TodoApiOAuth.Data;
using TodoApiOAuth.Todos.Entity;

namespace TodoApiOAuth.Todos.Repo;

public class TodoRepo(TodoContext db) : ITodoRepo
{
    public async Task<IEnumerable<Todo>> GetAllTodos() =>
    await db.Todos.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Todo>> GetDoneTodos() =>
    await db.Todos.Where(t => t.IsDone).AsNoTracking().ToListAsync();

    public async Task<Todo?> GetTodo(Guid id) => await db.Todos.FindAsync(id);

    public async Task<Todo> CreateTodo(Todo todo)
    {
        await db.Todos.AddAsync(todo);
        await db.SaveChangesAsync();
        return todo;
    }

    public async Task<Todo?> UpdateTodo(Guid id, Todo input)
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo is null) return null;
        todo.Title = input.Title;
        todo.IsDone = input.IsDone;
        await db.SaveChangesAsync();
        return todo;
    }

    public async Task<int> DeleteTodo(Guid id)
    {
        var todo = await db.Todos.FindAsync(id);
        if (todo is null) return default;
        db.Todos.Remove(todo);
        return await db.SaveChangesAsync();
    }
}