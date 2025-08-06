using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Services;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Reports.GetTransfersReport;
using DevsuApi.Features.Reports.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DevsuApi.Features.Reports.GetTransfersReportPdf;

public sealed class GetTransfersReportHandlerPdf(
    IClientRepository clientRepository,
    IPdfGeneratorService pdfGeneratorService,
    ILogger<GetTransfersReportHandlerPdf> logger,
    IValidator<GetTransfersReportQueryPdf> validator) : IRequestHandler<GetTransfersReportQueryPdf, Result<TransfersReportResponseAsPdf>>
{
    public async Task<Result<TransfersReportResponseAsPdf>> Handle(GetTransfersReportQueryPdf request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<TransfersReportResponseAsPdf>(new Error(
                GetTransfersReportErrorCodes.Validation,
                validationResult.ToString()));
        }

        var client = await clientRepository.GetByIdWithAccountsAndTransfersAsync(request.ClientId, request.StartDate, request.EndDate, cancellationToken);

        if (client is null || !client.HasTransfers)
        {
            return Result.Failure<TransfersReportResponseAsPdf>(new Error(
                GetTransfersReportErrorCodes.Null,
                "The list transfers for report was not found"));
        }

        IEnumerable<TransfersReportResponse> transfersReport = client.Accounts
            .SelectMany(a => a.Transfers, (a, t) => TransfersReportResponse.Create(client, a, t))
            .OrderBy(tr => tr.Date);

        try
        {
            var pdfBytes = pdfGeneratorService.GeneratePdf(transfersReport);
            var base64 = Convert.ToBase64String(pdfBytes);
            return Result.Success(TransfersReportResponseAsPdf.Create(base64));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, ex.Message);
            return Result.Failure<TransfersReportResponseAsPdf>(new Error("", ""));
        }
    }
}
