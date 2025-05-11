using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Database;
using Payment.API.Entities;
using Payment.API.Features.Status;

namespace Payment.API.Features.Payment.UpdateStatusById
{
    public sealed record UpdateStatusPaymentCommand(
    Guid Id,
    string StatusPrefix
    ) : ICommand<Result<UpdateStatusPaymentResult>>;

    public sealed record UpdateStatusPaymentResult(bool IsSuccess);

    public sealed class UpdateStatusCommandValidator
    : AbstractValidator<UpdateStatusPaymentCommand>
    {
        public UpdateStatusCommandValidator()
        {
            RuleFor(x => x.StatusPrefix)
               .NotEmpty()
               .WithMessage("StatusPrefix is required!")
               .IsEnumName(typeof(StatusEnum));
        }
    }

    public class UpdateStatusPaymentCommandHandler(ApplicationDbContext dbContext)
   : ICommandHandler<UpdateStatusPaymentCommand, Result<UpdateStatusPaymentResult>>
    {
        public async Task<Result<UpdateStatusPaymentResult>> Handle(UpdateStatusPaymentCommand command,
         CancellationToken cancellationToken)
        {
            Payments payment = await dbContext.Payment.FirstOrDefaultAsync(p => p.Id == command.Id, cancellationToken);
            if (payment is null)
                return Result.Failure<UpdateStatusPaymentResult>(new("Payment.NotFound", $"The payment with Id '{command.Id}' was not found"));

            Statuss status = await dbContext.Status.FirstOrDefaultAsync(s => s.Prefix == command.StatusPrefix, cancellationToken);
            if (status is null)
                return Result.Failure<UpdateStatusPaymentResult>(new("Status.NotFound", $"The status '{command.StatusPrefix}' was not found"));

            payment.StatusId = status.Id;
            payment.Updated = DateTime.UtcNow;

            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateStatusPaymentResult(true);
        }
    }
}
