using Carter;
using DevsuTestApi.Contracts.Clients;
using DevsuTestApi.Database;
using DevsuTestApi.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients;

public static class GetClients
{
    public class Query : IRequest<Result<List<ClientResponse>>>
    {
        // No parameters needed for this Query
        // Later we can implement a pagination or filtering mechanism if required
    }
    internal sealed class Handler(ApplicationDbContext dbContext) : IRequestHandler<Query, Result<List<ClientResponse>>>
    {
        public async Task<Result<List<ClientResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            var clients = await dbContext
                .Clients
                .AsNoTracking() // Use AsNoTracking for read-only queries
                .ProjectToType<ClientResponse>()
                .ToListAsync(cancellationToken);

            if (clients is null)
            {
                return Result.Failure<List<ClientResponse>>(new Error(
                    "GetClients.Null",
                    "The list clients was not found"));
            }

            return Result.Success(clients);
        }
    }
}

public class GetClientsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/clientes", async (ISender sender) =>
        {
            var query = new GetClients.Query();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NotFound(result.Error);
            }

            return Results.Ok(result.Value);
        });
    }
}
