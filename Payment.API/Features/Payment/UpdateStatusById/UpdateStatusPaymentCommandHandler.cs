using Microsoft.EntityFrameworkCore;
using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Database;
using Payment.API.Entities;
using Payment.API.Features.Payment.GetStatusById;

namespace Payment.API.Features.Payment.UpdateStatusById
{
    public sealed record UpdateStatusPaymentCommand(
    Guid Id,
    string Status
    ) : ICommand<Result<UpdateStatusPaymentResult>>;

    public sealed record UpdateStatusPaymentResult(bool isSuccess);

    public class UpdateStatusPaymentCommandHandler(ApplicationDbContext dbContext)
   : ICommandHandler<UpdateStatusPaymentCommand, Result<UpdateStatusPaymentResult>>
    {
        public async Task<Result<UpdateStatusPaymentResult>> Handle(UpdateStatusPaymentCommand command,
         CancellationToken cancellationToken)
        {
            Payments payment = await dbContext.Payment.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken: cancellationToken);

            if (payment is null)
                return Result.Failure<UpdateStatusPaymentResult>(new("Payment.NotFound", $"The payment with Id '{command.Id}' was not found"));

            Status status = await dbContext.Status.FirstOrDefaultAsync(s => s.Prefix == command.Status, cancellationToken: cancellationToken);

            if (status is null)
                return Result.Failure<UpdateStatusPaymentResult>(new("Status.NotFound", $"The status with Id '{command.Status}' was not found"));

            payment.StatusId = status.Id;
            payment.Updated = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateStatusPaymentResult(true);
        }
    }
}
