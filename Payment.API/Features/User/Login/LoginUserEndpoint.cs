using Carter;
using MediatR;
using Payment.API.Extensions;

namespace Payment.API.Features.User.Login
{
    public sealed record LoginUserRequest(string Email, string Password);
    public sealed record LoginUserResponse(string Token);
    public sealed class LoginUserEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/user/login", async (LoginUserRequest request, ISender sender) =>
            {
                var command = new LoginUserCommand(
                    request.Email,
                    request.Password);

                var result = await sender.Send(command);

                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("UserLogin")
            .Produces<LoginUserResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("UserLogin")
            .WithDescription("UserLogin");
        }
    }
}
