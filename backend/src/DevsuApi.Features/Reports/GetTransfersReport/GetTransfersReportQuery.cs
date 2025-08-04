using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public sealed class GetTransfersReportQuery : IRequest<Result<IEnumerable<TransfersReportResponse>>>
{
    public Guid ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
