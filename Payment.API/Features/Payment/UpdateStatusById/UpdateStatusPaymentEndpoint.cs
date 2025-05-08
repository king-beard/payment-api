using Carter;
using MediatR;
using Payment.API.Extensions;

namespace Payment.API.Features.Payment.UpdateStatusById
{
    public sealed record UpdateStatusPaymentRequest(
    string Status
    );
    public sealed record UpdateStatusPaymentResponse(Guid Id);

    public sealed class UpdateStatusPaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/payment/{id}/status", async (Guid id, UpdateStatusPaymentRequest request, ISender sender) =>
            {
                var command = new UpdateStatusPaymentCommand(
                    id,
                    request.Status);

                var result = await sender.Send(command);

                return result.Match(
                    onSuccess: () => Results.Ok(result.IsSuccess),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("UpdateStatusPayment")
            .Produces<UpdateStatusPaymentResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Status Payment")
            .WithDescription("Update Status Payment");
        }
    }
}
