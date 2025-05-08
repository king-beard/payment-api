using Carter;
using MediatR;
using Payment.API.Extensions;

namespace Payment.API.Features.Payment.Create
{
    public sealed record CreatePaymentRequest(
    string Concept,
    decimal Amount,
    int ProductsNumber,
    Guid StatusId
    );
    public sealed record CreatePaymentResponse(Guid Id);

    public sealed class CreatePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/payment", async (CreatePaymentRequest request, ISender sender) =>
            {
                var command = new CreatePaymentCommand(
                    request.Concept,
                    request.Amount,
                    request.ProductsNumber,
                    request.StatusId);

                var result = await sender.Send(command);

                return result.Match(
                    onSuccess: () => Results.Created($"api/products/{result.Value.Id}", result.Value),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("CreatePayment")
            .Produces<CreatePaymentResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Payment")
            .WithDescription("Create Payment");
        }
    }
}
