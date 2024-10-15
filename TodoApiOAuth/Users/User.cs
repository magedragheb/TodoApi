using TodoApiOAuth.Todos.Entity;

namespace TodoApiOAuth.Users.Entity;

public class User
{
    public Guid Id { get; init; }
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public List<Todo> Todos { get; set; } = [];
}
