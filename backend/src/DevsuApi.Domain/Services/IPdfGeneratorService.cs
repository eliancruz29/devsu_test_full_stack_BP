using DevsuApi.Domain.Entities;

namespace DevsuApi.Domain.Services;

public interface IPdfGeneratorService
{
    byte[] GeneratePdf(IEnumerable<TransfersReport> transfersReport);
}
