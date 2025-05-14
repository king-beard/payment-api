using FluentValidation;
using Payment.API.Features.Status;

namespace Payment.API.Features.Payment.UpdateStatusById;

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