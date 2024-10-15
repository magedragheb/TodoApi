using TodoApiOAuth.Users.Entity;

namespace TodoApiOAuth.Users.Repo;

public interface IUserRepo
{
    Task<User?> GetUserByUserName(string username);
    Task<User?> GetUserByEmail(string email);
    Task<User?> GetUserById(Guid id);
    Task<User> CreateUser(User user);
    Task<User?> UpdateUser(Guid id, User input);
    Task<int> DeleteUser(Guid id);
}