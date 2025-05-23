﻿using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Payment.API.Abstractions.CQRS;
using Payment.API.Abstractions.ResultResponse;
using Payment.API.Database;
using Payment.API.Entities;
using Payment.API.Features.Status;

namespace Payment.API.Features.Payment.Create
{
    public sealed record CreatePaymentCommand(
    string Concept,
    decimal Amount,
    int ProductsNumber,
    Guid ClientId,
    Guid ShopId,
    string StatusPrefix
    ) : ICommand<Result<CreatePaymentResult>>;

    public sealed record CreatePaymentResult(Guid Id);


    public sealed class CreateProductCommandValidator
    : AbstractValidator<CreatePaymentCommand>
    {
        public CreateProductCommandValidator()
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

    public class CreatePaymentCommandHandler(ApplicationDbContext dbContext)
   : ICommandHandler<CreatePaymentCommand, Result<CreatePaymentResult>>
    {
        public async Task<Result<CreatePaymentResult>> Handle(CreatePaymentCommand command,
         CancellationToken cancellationToken)
        {
            Clients client = await dbContext.Client.FirstOrDefaultAsync(p => p.Id == command.ClientId, cancellationToken);
            if (client is null)
                return Result.Failure<CreatePaymentResult>(new("Client.NotFound", $"The client with Id '{command.ClientId}' was not found"));

            Shops shop = await dbContext.Shop.FirstOrDefaultAsync(p => p.Id == command.ShopId, cancellationToken);
            if (shop is null)
                return Result.Failure<CreatePaymentResult>(new("Shop.NotFound", $"The shop with Id '{command.ShopId}' was not found"));

            Statuss status = await dbContext.Status.FirstOrDefaultAsync(s => s.Prefix == command.StatusPrefix, cancellationToken);
            if (status is null)
                return Result.Failure<CreatePaymentResult>(new("Status.NotFound", $"The status '{command.StatusPrefix}' was not found"));

            var payment = new Payments
            {
                Concept = command.Concept,
                Amount = command.Amount,
                ProductsNumber = command.ProductsNumber,
                ClientId = command.ClientId,
                ShopId = command.ShopId,
                StatusId = status.Id
            };

            dbContext.Payment.Add(payment);

            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreatePaymentResult(payment.Id);
        }
    }
}
