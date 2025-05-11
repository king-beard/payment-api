using Payment.API.Abstractions.ResultResponse;

namespace Payment.API.Abstractions.Errors;

public record Error(string Code, string? Description = default)
{
    public static readonly Error None = new(string.Empty);
    public static readonly Error Null = new("Error.NullValue", "The specified result value is null.");
    public static implicit operator Result(Error error) => Result.Failure(error);
    public static Error Problem(string code, string description) => new(code, description);
}
