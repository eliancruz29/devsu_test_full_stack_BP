namespace DevsuApi.Features.Reports.Shared;

public abstract class GetTransfersReportQueryBase
{
    public Guid ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
