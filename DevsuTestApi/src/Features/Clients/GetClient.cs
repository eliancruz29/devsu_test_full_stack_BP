using Carter;
using DevsuTestApi.Contracts.Clients;
using DevsuTestApi.Database;
using DevsuTestApi.Shared;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DevsuTestApi.Features.Clients;

public static class GetClient
{
    public class Query : IRequest<Result<ClientResponse>>
    {
        public Guid Id { get; set; }
    }
    internal sealed class Handler(ApplicationDbContext dbContext) : IRequestHandler<Query, Result<ClientResponse>>
    {
        public async Task<Result<ClientResponse>> Handle(Query request, CancellationToken cancellationToken)
        {
            var clientResponse = await dbContext
                .Clients
                .AsNoTracking() // Use AsNoTracking for read-only queries
                .Where(client => client.Id == request.Id)
                .ProjectToType<ClientResponse>()
                .FirstOrDefaultAsync(cancellationToken);

            if (clientResponse is null)
            {
                return Result.Failure<ClientResponse>(new Error(
                    "GetClient.Null",
                    "The client with the specified ID was not found"));
            }

            return clientResponse;
        }
    }
}

public class GetClientEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {        
        app.MapGet("api/clientes/{id}", async (Guid id, ISender sender) =>
        {
            var query = new GetClient.Query { Id = id };

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                return Results.NoContent();
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetClient")
        .WithTags("Clients")
        .Produces<ClientResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent);
    }
}
