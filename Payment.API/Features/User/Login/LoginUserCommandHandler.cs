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
            var user = new Users() { Email = "chucho_herrera10@icloud.com", PasswordHash = "0419E351541CC869B35D025FF86C35A547AC5D2552434DC64B3E3E81005B98B6-AABCBD19043E4BAFB859CA05EFAA6F36" };

            bool verified = PasswordHasher.Verify(command.Password, user.PasswordHash);

            if (!verified)
            {
                return Result.Failure<LoginUserCommandResult>(new("User not found!", ""));
            }

            string token = TokenProvider.Create(user, configuration);

            return new LoginUserCommandResult(token);
        }
    }
}
