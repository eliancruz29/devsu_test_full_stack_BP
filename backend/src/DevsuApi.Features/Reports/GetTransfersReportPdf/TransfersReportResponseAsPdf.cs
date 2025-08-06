using DevsuApi.Domain.Interfaces;

namespace DevsuApi.Features.Reports.GetTransfersReportPdf;

public class TransfersReportResponseAsPdf
{
    public string File { get; private set; } = string.Empty;

    public static TransfersReportResponseAsPdf Create(string file)
    {
        return new()
        {
            File = file
        };
    }
}
