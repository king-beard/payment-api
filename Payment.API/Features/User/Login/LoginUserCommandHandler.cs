using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Authentication;
using Payment.API.Entities;

namespace Payment.API.Features.User.Login
{
    public sealed record LoginUserCommand(string Email, string Password) : ICommand<Result<LoginUserCommandResult>>;
    public sealed record LoginUserCommandResult(string Token);

    public class LoginUserCommandHandler(IConfiguration configuration)
   : ICommandHandler<LoginUserCommand, Result<LoginUserCommandResult>>
    {
        public async Task<Result<LoginUserCommandResult>> Handle(LoginUserCommand command,
         CancellationToken cancellationToken)
        {
            var user = new Users() { Email = configuration["UsersAPI:Email"], PasswordHash = configuration["UsersAPI:PasswordHash"] };
            
            if (user.Email != command.Email)
                return Result.Failure<LoginUserCommandResult>(new("User", "Email doesn't match"));
            
            bool verified = PasswordHasher.Verify(command.Password, user.PasswordHash);
            if (!verified)
                return Result.Failure<LoginUserCommandResult>(new("User not found!", ""));

            string token = TokenProvider.Create(user, configuration);

            return new LoginUserCommandResult(token);
        }
    }
}
