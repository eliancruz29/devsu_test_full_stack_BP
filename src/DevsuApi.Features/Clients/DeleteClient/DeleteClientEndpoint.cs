using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DevsuApi.Domain.Exceptions;

namespace DevsuApi.Features.Clients.DeleteClient;

public class DeleteClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/clients/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new DeleteClientQuery { Id = id };

                await sender.Send(query);

                return Results.NoContent();
            }
            catch (ClientNotFoundException)
            {
                return Results.StatusCode(StatusCodes.Status304NotModified);
            }
        })
        .WithName("DeleteClient")
        .WithTags("Clients")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status304NotModified);
    }
}
