using Carter;
using DevsuApi.Features.Transfers.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Transfers.GetTransfer;

public class GetTransferEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/transfers/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetTransferQuery { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetTransfer")
        .WithTags("Transfers")
        .Produces<TransferResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
