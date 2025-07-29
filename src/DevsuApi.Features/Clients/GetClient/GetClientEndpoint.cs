using Carter;
using DevsuApi.Features.Clients.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Clients.GetClient;

public class GetClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/clientes/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetClientQuery { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetClient")
        .WithTags("Clients")
        .Produces<ClientResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
