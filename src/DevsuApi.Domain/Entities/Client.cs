using DevsuApi.Domain.Enums;

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
}