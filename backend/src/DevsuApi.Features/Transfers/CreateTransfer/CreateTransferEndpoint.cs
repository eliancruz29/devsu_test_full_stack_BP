using Carter;
using DevsuApi.Domain.Exceptions.Accounts;
using DevsuApi.Domain.Exceptions.Transfers;
using DevsuApi.Domain.Shared;
using FluentValidation;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace DevsuApi.Features.Transfers.CreateTransfer;

public class CreateTransferEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("api/transfers", async (CreateTransferRequest request, ISender sender, ILogger<CreateTransferEndpoint> looger) =>
        {
            try
            {
                var command = request.Adapt<CreateTransferCommand>();

                var result = await sender.Send(command);

                if (result.IsFailure)
                {
                    return Results.BadRequest(result.Error);
                }

                return Results.Ok(result.Value);
            }
            catch (AccountUnAvailableBalanceException ex)
            {
                looger.LogError(ex, ex.Message);
                return Results.BadRequest(
                    new Error(
                        "CreateTransfer.AccountUnAvailable",
                        ex.Message)
                );
            }
            catch (TransferMaxDailyLimitReachedException ex)
            {
                looger.LogError(ex, ex.Message);
                return Results.BadRequest(
                    new Error(
                        "CreateTransfer.TransferMaxDailyLimitReached",
                        ex.Message)
                );
            }
        })
        .WithName("CreateTransfer")
        .WithTags("Transfers")
        .Produces<Guid>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
