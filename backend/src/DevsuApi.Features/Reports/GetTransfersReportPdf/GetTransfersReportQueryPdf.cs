using DevsuApi.Domain.Shared;
using DevsuApi.Features.Reports.Shared;
using MediatR;

namespace DevsuApi.Features.Reports.GetTransfersReportPdf;

public sealed class GetTransfersReportQueryPdf : GetTransfersReportQueryBase, IRequest<Result<TransfersReportResponseAsPdf>>
{ }
