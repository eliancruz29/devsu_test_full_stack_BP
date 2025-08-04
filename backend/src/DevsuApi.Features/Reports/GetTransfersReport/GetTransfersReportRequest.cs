namespace DevsuApi.Features.Reports.GetTransfersReport;

public record GetTransfersReportRequest(Guid ClientId, DateTime StartDate, DateTime EndDate);
