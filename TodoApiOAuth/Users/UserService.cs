using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using TodoApiOAuth.Users.DTO;
using TodoApiOAuth.Users.Entity;
using TodoApiOAuth.Users.Repo;

namespace TodoApiOAuth.Users.Service;

public class UserService(
    IUserRepo repo,
    IHttpContextAccessor context) : IUserService
{
    public async Task<IResult> SignUp(UserDTO userDto)
    {
        var user = new User { UserName = userDto.UserName, Email = userDto.Email };
        await repo.CreateUser(user);
        if (context.HttpContext is null) return TypedResults.BadRequest();
        await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetPrincipal(user.Id.ToString()));
        return TypedResults.Ok(user);
    }

    public async Task<IResult> SignIn(UserDTO userDto)
    {
        var user = await repo.GetUserByEmail(userDto.UserName);
        if (user is null) return TypedResults.NotFound();
        if (context.HttpContext is null) return TypedResults.BadRequest();
        await context.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, GetPrincipal(user.Id.ToString()));
        return TypedResults.Ok(user);
    }

    public async Task<IResult> SignOut()
    {
        if (context.HttpContext is null) return TypedResults.BadRequest();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return TypedResults.Ok();
    }

    public ClaimsPrincipal GetPrincipal(string userId)
    {
        var identity = new ClaimsIdentity([new Claim("Id", userId)], CookieAuthenticationDefaults.AuthenticationScheme);
        return new ClaimsPrincipal(identity);
    }
}