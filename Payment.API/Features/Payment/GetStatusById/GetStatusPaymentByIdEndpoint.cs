using Carter;
using MediatR;
using Payment.API.Extensions;

namespace Payment.API.Features.Payment.GetStatusById
{

    public sealed record GetStatusPaymentByIdResponse(string status);

    public sealed class GetStatusPaymentByIdEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("api/payment/{id}/status", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new GetStatusPaymentByIdQuery(id));

                return result.Match(
                    onSuccess: () => Results.Ok(result.Value),
                    onFailure: error => Results.BadRequest(error));
            })
            .WithName("GetStatusPaymentById")
            .Produces<GetStatusPaymentByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Status Payment By Id")
            .WithDescription("Get Status Payment By Id");
        }
    }
}
