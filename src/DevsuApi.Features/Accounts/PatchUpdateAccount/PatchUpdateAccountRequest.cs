using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Accounts.PatchUpdateAccount;

public record PatchUpdateAccountRequest
(
    Guid Id,
    string? AccountNumber,
    AccountTypes? Type,
    int? OpeningBalance,
    Status? Status
);
