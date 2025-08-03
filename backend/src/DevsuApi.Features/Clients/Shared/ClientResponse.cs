using DevsuApi.Domain.Enums;
using Microsoft.OpenApi.Extensions;

namespace DevsuApi.Features.Clients.Shared;

public class ClientResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Gender Gender { get; set; }
    public string GenderName => Gender.GetDisplayName();
    public DateTime DateOfBirth { get; set; }
    public string Identification { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public Guid ClientId { get; set; }
    public string Password { get; set; } = string.Empty;
    public Status Status { get; set; }
}
