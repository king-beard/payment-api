using Payment.API.Abstractions.Errors;
using Payment.API.Abstractions.ResultResponse;

namespace Payment.API.Extensions
{
    public static class ResultExtensions
    {
        public static T Match<T>(
            this Result result,
            Func<T> onSuccess,
            Func<Error, T> onFailure)
        {
            return result.IsSuccess ? onSuccess() : onFailure(result.Error);
        }
    }
}
