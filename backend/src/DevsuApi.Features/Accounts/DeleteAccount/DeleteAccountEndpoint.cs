using Carter;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using DevsuApi.Domain.Exceptions.Accounts;

namespace DevsuApi.Features.Accounts.DeleteAccount;

public class DeleteAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/accounts/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                var query = new DeleteAccountQuery { Id = id };

                await sender.Send(query);

                return Results.NoContent();
            }
            catch (AccountNotFoundException)
            {
                return Results.StatusCode(StatusCodes.Status304NotModified);
            }
        })
        .WithName("DeleteAccount")
        .WithTags("Accounts")
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status304NotModified);
    }
}
