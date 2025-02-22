using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TodoApiOAuth.Data;
using TodoApiOAuth.Endpoints;
using TodoApiOAuth.Todos.Repo;
using TodoApiOAuth.Todos.Service;
using TodoApiOAuth.Users.Repo;
using TodoApiOAuth.Users.Service;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>(options =>
    options.UseSqlite("Data Source=Data/todo.db"));
builder.Services.AddTransient<ITodoService, TodoService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITodoRepo, TodoRepo>();
builder.Services.AddTransient<IUserRepo, UserRepo>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpoints(Assembly.GetExecutingAssembly());
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Denied";
        options.Cookie.MaxAge = TimeSpan.FromDays(3);
        options.Cookie.Name = "TodoApiOAuth";
    });
builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapEndpoints();

app.Run();
