using Carter;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Clients.UpdateClient;

public class UpdateClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("api/clientes/{id:guid}", async (Guid id, [FromBody] UpdateClientRequest request, ISender sender) =>
        {
            if (id != request.Id)
            {
                return Results.BadRequest(new { Message = "Id in URL does not match the request body." });
            }

            var command = request.Adapt<UpdateClientCommand>();
            command.Id = id;

            var result = await sender.Send(command);

            if (result.IsFailure)
            {
                return Results.BadRequest(result.Error);
            }

            return Results.NoContent();
        })
        .WithName("UpdateClient")
        .WithTags("Clients")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
