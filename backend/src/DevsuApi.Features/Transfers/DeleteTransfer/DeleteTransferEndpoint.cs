using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DevsuApi.Domain.Exceptions.Transfers;

namespace DevsuApi.Features.Transfers.DeleteTransfer;

public class DeleteTransferEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/transfers/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new DeleteTransferQuery { Id = id };

                await sender.Send(query);

                return Results.NoContent();
            }
            catch (TransferNotFoundException)
            {
                return Results.StatusCode(StatusCodes.Status304NotModified);
            }
        })
        .WithName("DeleteTransfer")
        .WithTags("Transfers")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status304NotModified);
    }
}
