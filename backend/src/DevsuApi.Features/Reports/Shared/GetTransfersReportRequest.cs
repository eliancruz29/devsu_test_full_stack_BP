namespace DevsuApi.Features.Reports.Shared;

public record GetTransfersReportRequest(Guid ClientId, DateTime StartDate, DateTime EndDate);
