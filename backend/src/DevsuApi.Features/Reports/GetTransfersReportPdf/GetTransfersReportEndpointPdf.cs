using Carter;
using DevsuApi.Features.Reports.GetTransfersReport;
using DevsuApi.Features.Reports.Shared;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace DevsuApi.Features.Reports.GetTransfersReportPdf;

public class GetTransfersReportEndpointPdf : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/reports/transfers/pdf", async ([AsParameters] GetTransfersReportRequest request, ISender sender) =>
        {
            var query = request.Adapt<GetTransfersReportQueryPdf>();

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
        .WithName("GetTransfersReportPdf")
        .WithTags("Reports")
        .Produces<IEnumerable<TransfersReportResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status400BadRequest);
    }
}
