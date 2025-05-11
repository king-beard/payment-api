using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Payment.API.Extensions;

namespace Payment.API.Features.Payment.Create
{
    public sealed record CreatePaymentRequest(
    string Concept,
    decimal Amount,
    int ProductsNumber,
    Guid ClientId,
    Guid ShopId,
    string StatusPrefix
    );
    public sealed record CreatePaymentResponse(Guid Id);

    public sealed class CreatePaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("api/payment", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (CreatePaymentRequest request, ISender sender) =>
            {
                var command = new CreatePaymentCommand(
                    request.Concept,
                    request.Amount,
                    request.ProductsNumber,
                    request.ClientId,
                    request.ShopId,
                    request.StatusPrefix);

                var result = await sender.Send(command);

                return result.Match(
                    onSuccess: () => Results.Created($"api/payment/{result.Value.Id}", result.Value),
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
