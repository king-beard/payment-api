using Microsoft.EntityFrameworkCore;
using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Database;
using Payment.API.Entities;

namespace Payment.API.Features.Payment.GetStatusById
{
    public sealed record GetStatusPaymentByIdQuery(Guid Id) : IQuery<Result<GetStatusPaymentByIdResult>>;
    public sealed record GetStatusPaymentByIdResult(string Status, decimal Amount, DateTime Created);

    public class GetStatusPaymentByIdQueryHandler(ApplicationDbContext dbContext)
   : IQueryHandler<GetStatusPaymentByIdQuery, Result<GetStatusPaymentByIdResult>>
    {
        public async Task<Result<GetStatusPaymentByIdResult>> Handle(GetStatusPaymentByIdQuery query,
         CancellationToken cancellationToken)
        {
            Payments payment = await dbContext.Payment.FirstOrDefaultAsync(p => p.Id == query.Id, cancellationToken);
            if (payment is null)
                return Result.Failure<GetStatusPaymentByIdResult>(new("Payment.NotFound", $"The payment with Id '{query.Id}' was not found"));

            Statuss status = await dbContext.Status.FirstOrDefaultAsync(s => s.Id == payment.StatusId, cancellationToken);
            if (status is null)
                return Result.Failure<GetStatusPaymentByIdResult>(new("Status.NotFound", $"The status with Id '{payment.StatusId}' was not found"));

            return new GetStatusPaymentByIdResult(status.Description, payment.Amount, payment.Created);
        }
    }
}
