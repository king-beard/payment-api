using FluentValidation;
using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Database;
using Payment.API.Entities;

namespace Payment.API.Features.Payment.Create
{
    public sealed record CreatePaymentCommand(
    string Concept,
    decimal Amount,
    int ProductsNumber,
    Guid ClientId,
    Guid ShopId,
    Guid StatusId
    ) : ICommand<Result<CreatePaymentResult>>;

    public sealed record CreatePaymentResult(Guid Id);


    public sealed class CreateProductCommandValidator
    : AbstractValidator<CreatePaymentCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Concept)
                .NotEmpty()
                .WithMessage("Name is required!");
            RuleFor(x => x.Amount)
                .NotEmpty()
                .WithMessage("Concept is required!");
            RuleFor(x => x.ProductsNumber)
                .NotEmpty()
                .WithMessage("ProductsNumber is required!");
            RuleFor(x => x.ClientId)
                .NotEmpty()
                .WithMessage("ClientId is required!");
            RuleFor(x => x.ShopId)
               .NotEmpty()
               .WithMessage("ShopId is required!");
            RuleFor(x => x.StatusId)
               .NotEmpty()
               .WithMessage("StatusId is required!");
        }
    }

    public class CreatePaymentCommandHandler(ApplicationDbContext dbContext)
   : ICommandHandler<CreatePaymentCommand, Result<CreatePaymentResult>>
    {
        public async Task<Result<CreatePaymentResult>> Handle(CreatePaymentCommand command,
         CancellationToken cancellationToken)
        {
            var payment = new Payments
            {
                Concept = command.Concept,
                Amount = command.Amount,
                ProductsNumber = command.ProductsNumber,
                ClientId = command.ClientId,
                ShopId = command.ShopId,
                StatusId = command.StatusId
            };

            dbContext.Payment.Add(payment);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.Id);
        }
    }
}
