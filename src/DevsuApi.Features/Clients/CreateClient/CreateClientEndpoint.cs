using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Clients.CreateClient;

public class CreateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/clientes", async (CreateClientRequest request, ISender sender) =>
        {
            var command = request.Adapt<CreateClientCommand>();

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.Ok(result.Value);
        })
        .WithName("CreateClient")
        .WithTags("Clients")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
