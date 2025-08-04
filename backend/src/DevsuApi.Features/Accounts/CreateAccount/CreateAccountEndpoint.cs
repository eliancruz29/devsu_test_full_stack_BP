using Carter;
using DevsuApi.Domain.Exceptions.Clients;
using DevsuApi.Domain.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DevsuApi.Features.Accounts.CreateAccount;

public class CreateAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/accounts", async (CreateAccountRequest request, ISender sender, ILogger<CreateAccountEndpoint> looger) =>
        {
            try
            {
                var command = request.Adapt<CreateAccountCommand>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }
            catch (AccountAlreadyExistsException ex)
            {
                looger.LogError(ex, ex.Message);
                return Results.BadRequest(
                    new Error(
                        "CreateAccount.AccountAlreadyExists",
                        ex.Message)
                );
            }
        })
        .WithName("CreateAccount")
        .WithTags("Accounts")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
