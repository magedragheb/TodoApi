using TodoApiOAuth.Todos.DTO;

namespace TodoApiOAuth.Todos.Service;

public interface ITodoService
{
    Task<IResult> GetAllTodos();
    Task<IResult> GetDoneTodos();
    Task<IResult> GetTodo(Guid id);
    Task<IResult> CreateTodo(TodoDTO todoDto);
    Task<IResult> UpdateTodo(Guid id, TodoDTO todoDto);
    Task<IResult> DeleteTodo(Guid id);
}