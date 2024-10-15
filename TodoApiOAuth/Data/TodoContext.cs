using Microsoft.EntityFrameworkCore;
using TodoApiOAuth.Todos.Entity;
using TodoApiOAuth.Users.Entity;

namespace TodoApiOAuth.Data;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Todo> Todos { get; set; }
}