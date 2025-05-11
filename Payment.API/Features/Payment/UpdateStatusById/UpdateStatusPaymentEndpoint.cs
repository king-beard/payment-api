using Carter;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Payment.API.Extensions;

namespace Payment.API.Features.Payment.UpdateStatusById
{
    public sealed record UpdateStatusPaymentRequest(
    string StatusPrefix
    );
    public sealed record UpdateStatusPaymentResponse(Guid Id);

    public sealed class UpdateStatusPaymentEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("api/payment/{id}/status", [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] async (Guid id, UpdateStatusPaymentRequest request, ISender sender) =>
            {
                var command = new UpdateStatusPaymentCommand(
                    id,
                    request.StatusPrefix);

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
