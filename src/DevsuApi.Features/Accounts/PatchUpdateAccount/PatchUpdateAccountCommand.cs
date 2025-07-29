using DevsuApi.Domain.Enums;
using DevsuApi.Features.Accounts.Shared;
using DevsuApi.Domain.Shared;
using MediatR;

namespace DevsuApi.Features.Accounts.PatchUpdateAccount;

public class PatchUpdateAccountCommand : IRequest<Result<AccountResponse>>
{
    public Guid Id { get; set; }
    public string? AccountNumber { get; set; }
    public AccountTypes? Type { get; set; }
    public int? OpeningBalance { get; set; }
    public Status? Status { get; set; }
}
