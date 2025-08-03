using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DevsuApi.Features.Clients.CreateClient;

public class CreateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/clients", async (CreateClientRequest request, ISender sender, ILogger<CreateClientEndpoint> looger) =>
        {
            try
            {
                var command = request.Adapt<CreateClientCommand>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }
            catch (ValidationException ex)
            {
                return Results.BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                looger.LogError(ex, "An error occurred while creating a client.");
                return Results.BadRequest("An unexpected error occurred.");
            }
        })
        .WithName("CreateClient")
        .WithTags("Clients")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
