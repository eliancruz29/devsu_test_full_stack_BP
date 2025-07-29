using DevsuApi.Domain.Entities;
using DevsuApi.Domain.Exceptions;
using DevsuApi.Domain.Interfaces;
using DevsuApi.Domain.Repositories;
using MediatR;

namespace DevsuApi.Features.Accounts.DeleteAccount;

public sealed class DeleteAccountHandler(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteAccountQuery>
{
    public async Task Handle(DeleteAccountQuery request, CancellationToken cancellationToken)
    {
        Account? existantAccount = await accountRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existantAccount is null)
        {
            throw new AccountNotFoundException(request.Id);
        }

        accountRepository.Remove(existantAccount);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
