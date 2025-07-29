using Carter;
using DevsuApi.Features.Accounts.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Accounts.GetListOfAccounts;

public class GetListOfAccountsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/accounts", async ([AsParameters] GetListOfAccountsRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetListOfAccountsQuery>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetListOfAccounts")
        .WithTags("Accounts")
        .Produces<List<AccountResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
