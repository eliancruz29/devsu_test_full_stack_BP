using DevsuApi.Domain.Shared;
using DevsuApi.Features.Reports.Shared;
using MediatR;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public sealed class GetTransfersReportQuery : GetTransfersReportQueryBase, IRequest<Result<IEnumerable<TransfersReportResponse>>>
{ }
