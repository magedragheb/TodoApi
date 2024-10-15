using Microsoft.EntityFrameworkCore;
using TodoApiOAuth.Data;
using TodoApiOAuth.Users.Entity;

namespace TodoApiOAuth.Users.Repo;

public class UserRepo(TodoContext db) : IUserRepo
{
    public async Task<User?> GetUserByUserName(string username) => 
    await db.Users.Where(u => u.UserName == username).AsNoTracking().FirstOrDefaultAsync();

    public async Task<User?> GetUserByEmail(string email) => 
    await db.Users.Where(u => u.Email == email).AsNoTracking().FirstOrDefaultAsync();

    public async Task<User?> GetUserById(Guid id) => await db.Users.FindAsync(id);

    public async Task<User> CreateUser(User user)
    {
        await db.Users.AddAsync(user);
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUser(Guid id, User input)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return null;
        user.UserName = input.UserName;
        await db.SaveChangesAsync();
        return user;
    }

    public async Task<int> DeleteUser(Guid id)
    {
        var user = await db.Users.FindAsync(id);
        if (user is null) return default;
        db.Users.Remove(user);
        return await db.SaveChangesAsync();
    }

}
