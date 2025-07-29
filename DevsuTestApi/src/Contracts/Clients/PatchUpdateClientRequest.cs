using DevsuTestApi.Enums;

namespace DevsuTestApi.Contracts.Clients;

public class PatchUpdateClientRequest
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public string? Identification { get; set; }
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Password { get; set; }
    public Status? Status { get; set; }
}
