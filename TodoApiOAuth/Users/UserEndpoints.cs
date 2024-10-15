using TodoApiOAuth.Endpoints;
using TodoApiOAuth.Users.DTO;
using TodoApiOAuth.Users.Service;

namespace TodoApiOAuth.Users.Endpoints;

public class UserEndpoints : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/users")
        .WithTags("Users")
        .WithOpenApi();

        group.MapPost("/signup", (IUserService service, UserDTO userDto) => service.SignUp(userDto)).WithSummary("Creates a new user and logs in");

        group.MapPost("/signin", (IUserService service, UserDTO userDto) => service.SignIn(userDto)).WithSummary("Logs in");

        group.MapPost("/signout", (IUserService service) => service.SignOut())
        .WithSummary("Logs out")
        .RequireAuthorization(a => a.RequireAuthenticatedUser());
    }
}