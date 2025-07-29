using Carter;
using DevsuApi.Features.Accounts.Shared;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Accounts.GetAccount;

public class GetAccountEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/accounts/{id:guid}", async (Guid id, ISender sender) =>
        {
            var query = new GetAccountQuery { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetAccount")
        .WithTags("Accounts")
        .Produces<AccountResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
