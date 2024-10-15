using TodoApiOAuth.Users.DTO;

namespace TodoApiOAuth.Users.Service;

public interface IUserService
{
    Task<IResult> SignUp(UserDTO userDto);
    Task<IResult> SignIn(UserDTO userDto);
    Task<IResult> SignOut();
}