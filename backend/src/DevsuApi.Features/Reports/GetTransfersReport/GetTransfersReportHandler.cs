using DevsuApi.Domain.Repositories;
using DevsuApi.Domain.Shared;
using DevsuApi.Features.Reports.Shared;
using FluentValidation;
using MediatR;

namespace DevsuApi.Features.Reports.GetTransfersReport;

public sealed class GetTransfersReportHandler(
    IClientRepository clientRepository,
    IValidator<GetTransfersReportQuery> validator) : IRequestHandler<GetTransfersReportQuery, Result<IEnumerable<TransfersReportResponse>>>
{
    public async Task<Result<IEnumerable<TransfersReportResponse>>> Handle(GetTransfersReportQuery request, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            return Result.Failure<IEnumerable<TransfersReportResponse>>(new Error(
                GetTransfersReportErrorCodes.Validation,
                validationResult.ToString()));
        }

        var client = await clientRepository.GetByIdWithAccountsAndTransfersAsync(request.ClientId, request.StartDate, request.EndDate, cancellationToken);

        if (client is null || !client.HasTransfers)
        {
            return Result.Failure<IEnumerable<TransfersReportResponse>>(new Error(
                GetTransfersReportErrorCodes.Null,
                "The list transfers for report was not found"));
        }

        IEnumerable<TransfersReportResponse> transfersReport = client.Accounts
            .SelectMany(a => a.Transfers, (a, t) => TransfersReportResponse.Create(client, a, t))
            .OrderBy(tr => tr.Date);

        return Result.Success(transfersReport);
    }
}
