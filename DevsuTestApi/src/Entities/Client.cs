using DevsuTestApi.Enums;

namespace DevsuTestApi.Entities;

public class Client : Person
{
    public Guid ClientId { get; set; }

    public string Password { get; set; } = string.Empty;

    public Status Status { get; set; }

    
}