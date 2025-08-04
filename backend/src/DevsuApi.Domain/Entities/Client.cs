using DevsuApi.Domain.Enums;
using DevsuApi.Domain.Exceptions.Clients;

namespace DevsuApi.Domain.Entities;

public sealed class Client : Person
{
    private readonly List<Account> _accounts = [];

    private Client(
        Guid id,
        string name,
        Gender gender,
        DateTime dateOfBirth,
        string identification,
        string address,
        string phoneNumber,
        Guid clientId,
        string password,
        Status status
    ) : base(
        id,
        name,
        gender,
        dateOfBirth,
        identification,
        address,
        phoneNumber)
    {
        ClientId = clientId;
        Password = password;
        Status = status;
    }

    public Guid ClientId { get; private init; }

    public string Password { get; private set; } = string.Empty;

    public Status Status { get; private set; }

    public IReadOnlyCollection<Account> Accounts => _accounts;

    public bool HasTransfers => _accounts.Any(a => a.Transfers.Count != 0);

    public static Client Create(
        string name,
        Gender gender,
        DateTime dateOfBirth,
        string identification,
        string address,
        string phoneNumber,
        string password)
    {
        return new(
            Guid.NewGuid(),
            name,
            gender,
            dateOfBirth,
            identification,
            address,
            phoneNumber,
            Guid.NewGuid(),
            password,
            Status.Active);
    }

    public void Update(
        string name,
        Gender gender,
        DateTime dateOfBirth,
        string identification,
        string address,
        string phoneNumber,
        string password,
        Status status)
    {
        Update(
            name,
            gender,
            dateOfBirth,
            identification,
            address,
            phoneNumber);

        Password = password;
        Status = status;
    }

    public Account AddAccount(
         string accountNumber,
         AccountTypes type,
         int openingBalance)
    {
        bool accountExist = _accounts.Any(a => a.AccountNumber == accountNumber);
        if (accountExist)
        {
            throw new AccountAlreadyExistsException(Id, accountNumber);
        }

        Account newAccount = Account.Create(
            Id,
            accountNumber,
            type,
            openingBalance);

        _accounts.Add(newAccount);

        return newAccount;
    }
}