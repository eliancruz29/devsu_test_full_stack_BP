using Carter;
using DevsuApi.Features.Transfers.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Transfers.GetListOfTransfers;

public class GetListOfTransfersEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/transfers", async ([AsParameters] GetListOfTransfersRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetListOfTransfersQuery>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetListOfTransfers")
        .WithTags("Transfers")
        .Produces<List<TransferResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
