using DevsuTestApi.Enums;

namespace DevsuTestApi.Features.Clients.Shared;

public class ClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid ClientId { get; private set; }
    public string Password { get; private set; } = string.Empty;
    public Status Status { get; private set; }
}
