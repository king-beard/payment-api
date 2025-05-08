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
    Guid StatusId
    ) : ICommand<Result<CreatePaymentResult>>;

    public sealed record CreatePaymentResult(Guid Id);

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
                StatusId = command.StatusId
            };

            dbContext.Payment.Add(payment);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.Id);
        }
    }
}
