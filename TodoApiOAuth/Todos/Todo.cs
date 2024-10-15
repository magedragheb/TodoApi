namespace TodoApiOAuth.Todos.Entity;

public class Todo
{
    public Guid Id { get; init; }
    public required string Title { get; set; }
    public bool IsDone { get; set; } = false;
    public Guid UserId { get; set; }
}