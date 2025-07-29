using MediatR;

namespace DevsuApi.Features.Accounts.DeleteAccount;

public sealed class DeleteAccountQuery : IRequest
{
    public Guid Id { get; set; }
}
