using Carter;
using DevsuApi.Features.Clients.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Clients.GetListOfClients;

public class GetListOfClientsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/clientes", async (ISender sender) =>
        {
            var query = new GetListOfClientsQuery();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetListOfClients")
        .WithTags("Clients")
        .Produces<List<ClientResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
