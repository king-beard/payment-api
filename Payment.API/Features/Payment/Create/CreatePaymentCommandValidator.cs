using FluentValidation;
using Payment.API.Features.Status;

namespace Payment.API.Features.Payment.Create;

public sealed class CreatePaymentCommandValidator
    : AbstractValidator<CreatePaymentCommand>
{
    public CreatePaymentCommandValidator()
    {
        RuleFor(x => x.Concept)
            .NotEmpty()
            .WithMessage("Concept is required!");
        RuleFor(x => x.Amount)
            .NotEmpty()
            .WithMessage("Amount is required!");
        RuleFor(x => x.ProductsNumber)
            .NotEmpty()
            .WithMessage("ProductsNumber is required!");
        RuleFor(x => x.ClientId)
            .NotEmpty()
            .WithMessage("ClientId is required!");
        RuleFor(x => x.ShopId)
            .NotEmpty()
            .WithMessage("ShopId is required!");
        RuleFor(x => x.StatusPrefix)
            .NotEmpty()
            .WithMessage("StatusPrefix is required!")
            .IsEnumName(typeof(StatusEnum));
    }
}