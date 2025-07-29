using DevsuApi.Domain.Enums;

namespace DevsuApi.Features.Accounts.CreateAccount;

public record CreateAccountRequest
(
    Guid ClientId,
    string AccountNumber,
    AccountTypes Type,
    int OpeningBalance,
    Status Status
);
