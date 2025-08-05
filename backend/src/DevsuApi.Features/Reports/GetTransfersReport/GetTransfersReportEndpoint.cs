using Carter;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public class GetTransfersReportEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/reports/transfers", async ([AsParameters] GetTransfersReportRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetTransfersReportQuery>();

            var result = await sender.Send(query);

            if (result.IsFailure)
            {
                if (GetTransfersReportErrorCodes.Validation.Equals(result.Error.Code))
                {
                    return Results.BadRequest(result.Error.Message);
                }
                else
                {
                    return Results.NoContent();
                }
            }

            return Results.Ok(result.Value);
        })
        .WithName("GetTransfersReport")
        .WithTags("Reports")
        .Produces<IEnumerable<TransfersReportResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
