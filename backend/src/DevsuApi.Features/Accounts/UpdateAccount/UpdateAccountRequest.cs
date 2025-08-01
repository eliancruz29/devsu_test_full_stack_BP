using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Accounts.UpdateAccount;

public record UpdateAccountRequest
(
    Guid Id,
    string AccountNumber,
    AccountTypes Type,
    int OpeningBalance,
    Status Status
);
